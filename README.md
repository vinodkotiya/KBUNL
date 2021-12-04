# KBUNL
NTPC Kanti Intranet
![Landing Page](https://github.com/vinodkotiya/KBUNL/blob/master/screenshot1.png)

Installation Steps
Install SQL Express 2016
Install SQL Server Management Studio
Import/ Restore Database DB_Intranet_KBUNL (Get .bak file in zip folder in root)
Create new Database User db_intranet from security > Login > New User
Create username double click > User Mapping > select database > choose db_owner and public

Configure IIS. Add IIS Roles.
Check ASP.NET option. After clicking on ASP.NET option ISAPI Filters, ISAPI Extensions, .NET Extensibility options will be selected automatically.

IIS Create new website > unassigned ip


In web.config correct the connection string for database.

Data Source= whatever server name 
Initial Catalog = database name



#######################
See Backup process pdf in root folder.

Contract Payment system
![CPS](https://github.com/vinodkotiya/KBUNL/blob/master/screenshot2.png)

IT Department
![IT](https://github.com/vinodkotiya/KBUNL/blob/master/screenshot6.png)

OCMS
![ocms](https://github.com/vinodkotiya/KBUNL/blob/master/screenshot3.png)

Network Monitoring
![Net](https://github.com/vinodkotiya/KBUNL/blob/master/screenshot4.png)

Contract Closing
![CC](https://github.com/vinodkotiya/KBUNL/blob/master/screenshot5.png)
