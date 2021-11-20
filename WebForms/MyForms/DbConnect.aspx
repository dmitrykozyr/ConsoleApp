<!-- 
    Чтобы добавить БД, в проект добавляем новый айтем SQL Server Database
    На вкладке Server Explorer в нашу БД добавляем таблицу, столбцы и нажимаем вверху Update -> Update Database
    Добавляем на форму Data -> SqlDataSource и GridView
    В настройках SqlDataSource в дизайнере выбираем нашу БД и все стандартное
    В найтройках GridView в дизайнере выбираем наш SqlDataSource
    
    Жизненный цикл страницы:
    - Init Phase:   PreInit -> Init -> InitComplete -> LoadState -> ProcessPostData
    - Load Phase:   PreLoad -> Load -> ProcessPostData (Second Try) -> ChangeEvents (PostBackEvent) -> LoadComplete
    - Render Phase: PreRender -> PreRenderComplete -> SaveState -> SaveStateComplete -> Render

    БД
    - Чтобы добавить БД, на папке App_Data жмем ПКМ -> SQL Server Database
    - Если нужно подключиться к существующей БД за пределами проекта, переходим на вкрадку Server Explorer в VS ->
      ПКМ на DataConnections -> Create New SQL Server Database -> указываем Server name: (localDB)\MSSQLLocalDB,
      database name DemoDB. Новая БД появится в списке и если нажать на нее ПКМ -> Свойства, можно увидеть Connection String,
      которую можно добавить в файл Web.config и в пути абслютный путь C:\Users\dmitr\source\repos\SharpEdu\WebForms\App_Data меняем на |DataDirectory|

-->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebForms.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 117px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Dima is Amazing</h1>
        <table class="auto-style1">
            <tr>
                <td colspan="2" style="text-align: center; font-weight: 700">WebForm with database</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">Id</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Name</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>
                    <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="BtnInsert_Click" Width="70px" />
                &nbsp;&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" OnClick="BtnUpdate_Click" Text="Update" Width="70px" />
&nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" OnClick="BtnDelete_Click" Text="Delete" Width="70px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" Text="Message"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" OnClick="BtnSearch_Click" Text="Search" Width="70px" />
&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [TableWebForms]"></asp:SqlDataSource>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        </Columns>
                    </asp:GridView>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
