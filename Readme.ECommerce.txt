�������� �������: ASP.NET Core Web Application
��� ECommerce.Api.Products
�������� API, ������� ������� Configure for HTTPS

�������� �� �������� ��� -> Set startup project -> �������� ������ ������� ��� Startup, ����� ��� ��� ����������� ��� ������ ���������

������ Products.Test ������ �� ������ xUnit .Net Core
��������� ������ �� ������ Products, �.�. ����� ��� �����������

CI/CD
����� ���������������� ������ ������, �������� ������ �� https://azure.microsoft.com/en-us/services/devops/
https://dmitrykozyr.visualstudio.com/Ecommerce
https://www.linkedin.com/learning/azure-microservices-with-dot-net-core-for-developers/creating-an-azure-devops-project?autoAdvance=true&autoSkip=true&autoplay=true&resume=false&u=2113185
�������� Repos -> Import -> ����������� ��� �����������
�������� Pipelines -> New Pipeline -> �������� ����������� ����������� -> Continue -> � ������ �������� ���� .Net Core
��������� Job ���� Use .Net Core -> � ������������� ����� -> ������ ������ 3.1.x
�������� Publish � ������� ������� Publish Web Projects
�������� Save & queue -> Save and run
����� �������� CI, ��������� �� Pipelines -> Edit -> �� ������� Triggers ������� Enable continious integration -> Save & queue -> Save and run
������ ���� ����� ����������� ��� ������ ������� � ������

Docker
����� �������� ����������� ���������������, �� ������ �� ������� �������� �������� ��� -> Add -> Container Orchestrator Support -> Docker Compose -> Windows
����� ����� � ����� � �������� ��������� dockerfile, � � ����� � ��������� ��������� docker-compose, ��� �������������� ������ ����������� ������
���-�� ��� ������� �������� �� ��������� ����� docker-compose
�� ���� ������� �������� � dockerfile 1903 ������ 1803 ��� �������������
� ����� docker-compose.override.yml ������ ����� ��� ������ �������� 600x
� ������� Orders -> Properties -> launchSettings.json ������� ������ "sslPort": 0
�� ����� �� ���������� -  ��� ������� docker-compose ������ ������
