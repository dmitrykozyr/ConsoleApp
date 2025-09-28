using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.Models.Dto;
using Domain.Models.Options;
using Domain.Models.RequentModels;
using Domain.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Domain.Services.API;

public class FilesService : IFilesService
{
    private readonly GeneralOptions? GeneralOptions;
    private readonly FileStorageOptions? FileStorageOptions;

    private readonly ILogging _logging;

    public FilesService(
        IOptions<GeneralOptions> generalOptions,
        IOptions<FileStorageOptions> fileStorageOptions,
        ILogging logging)
    {
        GeneralOptions = generalOptions.Value;
        FileStorageOptions = fileStorageOptions.Value;

        _logging = logging;
    }

    public FileStreamResponse GetFileStream(FileStorageRequest model)
    {
        Guard.IsNotNull(GeneralOptions);
        Guard.IsNotNull(FileStorageOptions);
                
        var fileStorageDTO = new FileStorageDto
        {
            Guid        = model.Guid,
            BucketPath  = model.BucketPath,
            //Token       = token
        };

        string sURL = $"{GeneralOptions.PostUrl}/{model.BucketPath}/{model.Guid}";

        var request = (HttpWebRequest)WebRequest.Create(sURL);
        request.Method = "GET";
        request.KeepAlive = true;

        // Добавление токена в Headers
        //request.Headers.Add("Authorization", "Bearer " + fileStorageDTO.Token.access_token);

        var response = request.GetResponse();
        var responseStream = response.GetResponseStream();

        string fileName = $"{model.Guid}";
        string fileExtension = GetFileExtension(response);

        var result = new FileStreamResponse
        {
            Stream = responseStream,
            FileNameExtension = $"{fileName}{fileExtension}"
        };

        return result;
    }

    public LoadFileResponse? GetFileByPath(FileStorageRequest model)
    {
        Guard.IsNotNull(GeneralOptions);
        Guard.IsNotNull(FileStorageOptions);

        var fileStorageDTO = new FileStorageDto
        {
            Guid        = model.Guid,
            BucketPath  = model.BucketPath,
            //Token       = token
        };

        string sURL = $"{GeneralOptions.PostUrl}/{model.BucketPath}/{model.Guid}";

        Guard.IsNotNullOrEmpty(sURL);

        var request = (HttpWebRequest)WebRequest.Create(sURL);
        request.Method = "GET";
        request.KeepAlive = true;

        // Добавление токена в Headers
        //request.Headers.Add("Authorization", "Bearer " + fileStorageDTO.Token.access_token);

        var result = new LoadFileResponse();

        try
        {
            using (WebResponse? response = request.GetResponse())
            {
                if (response is not null)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            Guard.IsNotNull(FileStorageOptions);
                            Guard.IsNotNull(FileStorageOptions.FileDownloadPath);

                            string fileName = $"{model.Guid}";
                            string fileExtension = GetFileExtension(response);
                            string fileFullName = Path.Combine(FileStorageOptions.FileDownloadPath, $"{fileName}{fileExtension}");

                            using (var fileStream = new FileStream(fileFullName, FileMode.Create, FileAccess.Write))
                            {
                                responseStream.CopyTo(fileStream);
                            }

                            return new LoadFileResponse
                            {
                                FileName = fileName,
                                FilePath = FileStorageOptions.FileDownloadPath
                            };
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            using (var stream = ex?.Response?.GetResponseStream())
            {
                if (stream is not null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var errorResponse = reader.ReadToEnd();

                        result.ErrorMessage = $"Ошибка получения файла, {errorResponse}";

                        _logging.LogToFile(result.ErrorMessage);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            result.ErrorMessage = $"Ошибка получения файла, {ex.Message}";
        }

        return result;
    }


    public async Task<Guid> LoadFileByBytesArray(LoadFileByBytesRequest model)
    {
        Guard.IsNotNull(GeneralOptions);
        Guard.IsNotNull(FileStorageOptions);

        var loadFileDTO = new LoadFileDto
        {
            BucketPath      = model.BucketPath,
            DeathTime       = model.DeathTime,
            LifeTimeHours   = model.LifeTimeHours,
            File            = Convert.FromBase64String(model.File ?? "")
        };

        string sURL = $"{GeneralOptions.PostUrl}/{loadFileDTO.BucketPath}";

        Guard.IsNotNullOrEmpty(sURL);

        var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
        var request = (HttpWebRequest)WebRequest.Create(sURL);
        request.Method = "POST";
        request.ContentType = "multipart/form-data; boundary=" + boundary;
        request.Accept = "application/json;charset=UTF-8";

        // Добавление токена в Headers
        //request.Headers.Add("Authorization", "Bearer " + token.access_token);

        using (var stream = request.GetRequestStream())
        {
            WriteFormData(stream, boundary, "deathTime", model.DeathTime?.ToString() ?? "");
            WriteFormData(stream, boundary, "lifeTimeHours", model.LifeTimeHours.ToString() ?? "");

            stream.Write(Encoding.UTF8.GetBytes($"\r\n--{boundary}\r\n"));
            stream.Write(Encoding.UTF8.GetBytes($"Content-Disposition: form-data; name=\"file\"; filename=\"{model.FileName}\"\r\n"));
            stream.Write(Encoding.UTF8.GetBytes("Content-Type: application/octet-stream\r\n\r\n"));

            await stream.WriteAsync(loadFileDTO.File, 0, loadFileDTO.File.Length);

            await stream.WriteAsync(Encoding.UTF8.GetBytes($"\r\n--{boundary}--\r\n"));
        }

        try
        {
            // После записи всех данных в поток, данные отправляются на сервер
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string readerResult = reader.ReadToEnd();

                var result = JsonConvert.DeserializeObject<UploadFileResponse>(readerResult);

                return result.Id;
            }
        }
        catch (WebException webEx)
        {
            using (var stream = webEx.Response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var errorResponse = reader.ReadToEnd();

                _logging.LogToFile($"Ошибка получения файла, {errorResponse}");
            }
        }
        catch (Exception ex)
        {
            _logging.LogToFile($"Ошибка получения файла, {ex.Message}");
        }

        return default;
    }

    public Guid LoadFileFromFileSystemByPath(LoadFileByPathRequest model)
    {
        Guard.IsNotNull(GeneralOptions);
        Guard.IsNotNull(FileStorageOptions);

        var loadFileDTO = new LoadFileDto
        {
            BucketPath = model.BucketPath,
            DeathTime = model.DeathTime,
            LifeTimeHours = model.LifeTimeHours,
        };

        string sURL = $"{GeneralOptions.PostUrl}/{loadFileDTO.BucketPath}";

        Guard.IsNotNullOrEmpty(sURL);

        var request = (HttpWebRequest)WebRequest.Create(sURL);
        request.Method      = "POST";
        request.ContentType = "multipart/form-data; boundary=---------------------------" + DateTime.Now.Ticks.ToString("x");
        request.Accept      = "application/json;charset=UTF-8";

        // Добавление токена в Headers
        //request.Headers.Add("Authorization", "Bearer " + token.access_token);

        // Открытие потока для записи данных в запрос
        using (var stream = request.GetRequestStream())
        {
            var boundary = request.ContentType.Split('=')[1];

            // Взято из Swagger
            var metaJson =
                "{" +
                    "\"k_login\": \"k-63546\"," +
                    "\"doc_type\": \"b\"," +
                    "\"master_id\": \"34120bfa-189c-47a5-bdf3-a3d3e2b61a42\"," +
                    "\"reportdate\": \"2021-08-06\"," +
                    "\"agreementid\": \"343aecae-2238-4da4-9a5f-c6987e2372f8\"" +
                "}";

            // Взято из Swagger
            var aclJson =
                "[{" +
                    "\"realm\": \"Broker\"," +
                    "\"subjectType\": \"MASTER_ID\"," +
                    "\"subject\": \"107f17fb-f2ae-4b3d-8936-7f20d78404a2\"" +
                "}]";

            WriteFormData(stream, boundary, "deathTime", model.DeathTime ?? "");
            WriteFormData(stream, boundary, "lifeTimeHours", model.LifeTimeHours.ToString() ?? "");
            WriteFormData(stream, boundary, "meta", metaJson);
            WriteFormData(stream, boundary, "acl", aclJson);

            stream.Write(Encoding.UTF8.GetBytes($"\r\n--{boundary}\r\n"));

            stream.Write(Encoding.UTF8.GetBytes($"Content-Disposition: form-data; name=\"file\"; filename=\"{Path.GetFileName(model.FilePathInFileSystem)}\"\r\n"));

            stream.Write(Encoding.UTF8.GetBytes("Content-Type: text/plain\r\n\r\n"));

            stream.Write(File.ReadAllBytes(model.FilePathInFileSystem ?? ""));

            stream.Write(Encoding.UTF8.GetBytes($"\r\n--{boundary}--\r\n"));
        }

        try
        {
            // После записи всех данных в поток, данные отправляются на сервер
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string readerResult = reader.ReadToEnd();

                var result = JsonConvert.DeserializeObject<UploadFileResponse>(readerResult);

                return result.Id;
            }
        }
        catch (WebException ex)
        {
            Guard.IsNotNull(ex.Response);

            using (var reader = new StreamReader(ex.Response.GetResponseStream()))
            {
                string errorResponse = reader.ReadToEnd();

                _logging.LogToFile($"Ошибка загрузки файла: {errorResponse}");
            }
        }
        catch (Exception ex)
        {
            _logging.LogToFile($"Ошибка загрузки файла: {ex.Message}");
        }

        return default;
    }

    public async Task<Guid> LoadFileFromFileSystemBySelection(LoadFileBySelectionRequest model, IFormFile file)
    {
        Guard.IsNotNull(GeneralOptions);
        Guard.IsNotNull(FileStorageOptions);

        var loadFileDTO = new LoadFileDto
        {
            BucketPath = model.BucketPath,
            DeathTime = model.DeathTime,
            LifeTimeHours = model.LifeTimeHours,
        };

        string sURL = $"{GeneralOptions.PostUrl}/{loadFileDTO.BucketPath}";

        Guard.IsNotNullOrEmpty(sURL);

        var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
        var request = (HttpWebRequest)WebRequest.Create(sURL);
        request.Method = "POST";
        request.ContentType = "multipart/form-data; boundary=" + boundary;
        request.Accept = "application/json;charset=UTF-8";

        // Добавление токена в Headers
        //request.Headers.Add("Authorization", "Bearer " + token.access_token);

        using (var stream = request.GetRequestStream())
        {
            WriteFormData(stream, boundary, "deathTime", model.DeathTime?.ToString() ?? "");
            WriteFormData(stream, boundary, "lifeTimeHours", model.LifeTimeHours.ToString() ?? "");

            stream.Write(Encoding.UTF8.GetBytes($"\r\n--{boundary}\r\n"));
            stream.Write(Encoding.UTF8.GetBytes($"Content-Disposition: form-data; name=\"file\"; filename=\"{Path.GetFileName(file.FileName)}\"\r\n"));
            stream.Write(Encoding.UTF8.GetBytes("Content-Type: application/octet-stream\r\n\r\n"));

            await file.CopyToAsync(stream);

            await stream.WriteAsync(Encoding.UTF8.GetBytes($"\r\n--{boundary}--\r\n"));
        }

        try
        {
            // После записи всех данных в поток, данные отправляются на сервер
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string readerResult = reader.ReadToEnd();

                var result = JsonConvert.DeserializeObject<UploadFileResponse>(readerResult);

                return result.Id;
            }
        }
        catch (WebException ex)
        {
            Guard.IsNotNull(ex.Response);

            using (var reader = new StreamReader(ex.Response.GetResponseStream()))
            {
                string errorResponse = reader.ReadToEnd();

                _logging.LogToFile($"Ошибка загрузки файла: {errorResponse}");
            }
        }
        catch (Exception ex)
        {
            _logging.LogToFile($"Ошибка загрузки файла: {ex.Message}");
        }

        return default;
    }

    public bool DeleteFile(FileStorageRequest model)
    {
        Guard.IsNotNull(GeneralOptions);
        Guard.IsNotNull(FileStorageOptions);

        var fileStorageDTO = new FileStorageDto
        {
            Guid = model.Guid,
            BucketPath = model.BucketPath,
            //Token = token
        };

        string sURL = $"{GeneralOptions.PostUrl}/{model.BucketPath}/{model.Guid}";

        Guard.IsNotNullOrEmpty(sURL);

        var request = (HttpWebRequest)WebRequest.Create(sURL);
        request.Method = "DELETE";
        request.KeepAlive = true;

        // Добавление токена в Headers
        //request.Headers.Add("Authorization: Bearer " + fileStorageDTO.Token.access_token);

        try
        {
            using (WebResponse? response = request.GetResponse())
            {
                if (response is not null)
                {
                    return true;
                }
            }
        }
        catch (WebException webEx)
        {
            using (var stream = webEx.Response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var errorResponse = reader.ReadToEnd();

                var result = new LoadFileResponse
                {
                    ErrorMessage = $"Ошибка получения файла, {errorResponse}"
                };

                _logging.LogToFile(result.ErrorMessage);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return false;
    }


    private void WriteFormData(Stream stream, string boundary, string name, string value)
    {
        // Метод записи полей формы в поток запроса с параметрами:
        // - stream   Поток запроса, в который будут записаны данные формы
        // - boundary Граница для разделения частей данных в multipart запросе - уникальная строка, позволяющая серверу определить, где заканчивается одно поле и начинается другое
        // - name     Имя поля формы, которое сервер ожидает в запросе (например, 'deathTime', 'lifeTimeHours')
        // - value    Значение поля формы

        stream.Write(Encoding.UTF8.GetBytes($"\r\n--{boundary}\r\n"));
        stream.Write(Encoding.UTF8.GetBytes($"Content-Disposition: form-data; name=\"{name}\"\r\n\r\n"));
        stream.Write(Encoding.UTF8.GetBytes(value));
    }

    private string GetFileExtension(WebResponse response)
    {
        string fileExtension = ".txt";

        // Проверяем заголовок Content-Disposition
        if (response.Headers["Content-Disposition"] != null)
        {
            var contentDisposition = response.Headers["Content-Disposition"];

            var fileNamePart = contentDisposition?
                .Split(';')
                .Select(part => part.Trim())
                .FirstOrDefault(part => part.StartsWith("filename=", StringComparison.OrdinalIgnoreCase));

            if (fileNamePart != null)
            {
                fileNamePart = fileNamePart
                    .Substring("filename=".Length)
                    .Trim('"');

                fileExtension = Path.GetExtension(fileNamePart);
            }
        }
        else if (response.Headers["Content-Type"] != null)
        {
            // Если Content-Disposition отсутствует, можно использовать Content-Type
            var contentType = response.Headers["Content-Type"];

            if (contentType != null)
            {
                if (contentType.Contains("text/plain"))
                {
                    fileExtension = ".txt";
                }
                else if (contentType.Contains("application/pdf"))
                {
                    fileExtension = ".pdf";
                }
                // Можно добавить другие типы файлов по мере необходимости
            }
        }

        return fileExtension;
    }
}
