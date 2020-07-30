using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Excel;
using DevExpress.DataAccess.Sql;
using System;
using System.Web.Hosting;

namespace NeuraDashboards {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            DashboardFileStorage dashboardFileStorage = new DashboardFileStorage("~/App_Data/Dashboards");
            ASPxDashboard1.SetDashboardStorage(dashboardFileStorage);

            // Uncomment this string to allow end users to create new data sources based on predefined connection strings.
            //ASPxDashboard1.SetConnectionStringsProvider(new DevExpress.DataAccess.Web.ConfigFileConnectionStringsProvider());
            
            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
            
            // Registers an SQL data source.
            DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "NWindConnectionString");
            SelectQuery query = SelectQueryFluentBuilder
                .AddTable("SalesPerson")
                .SelectAllColumns()
                .Build("Sales Person");
            sqlDataSource.Queries.Add(query);
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());

            DashboardSqlDataSource sqlDataSource2 = new DashboardSqlDataSource("SQL Data Source 2", "NeuraConnectionString");
            SelectQuery query2 = SelectQueryFluentBuilder
                .AddTable("ConcessionArea")
                .SelectAllColumns()
                .Build("Concession Area");
            sqlDataSource.Queries.Add(query2);
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource2.SaveToXml());



            // Registers an Object data source.
            DashboardObjectDataSource objDataSource = new DashboardObjectDataSource("Object Data Source");
            dataSourceStorage.RegisterDataSource("objDataSource", objDataSource.SaveToXml());
            
            // Registers an Excel data source.
            DashboardExcelDataSource excelDataSource = new DashboardExcelDataSource("Excel Data Source");
            excelDataSource.FileName = HostingEnvironment.MapPath(@"~/App_Data/Sales.xlsx");
            excelDataSource.SourceOptions = new ExcelSourceOptions(new ExcelWorksheetSettings("Sheet1"));
            dataSourceStorage.RegisterDataSource("excelDataSource", excelDataSource.SaveToXml());
            
            ASPxDashboard1.SetDataSourceStorage(dataSourceStorage);
            ASPxDashboard1.SetConnectionStringsProvider(new DevExpress.DataAccess.Web.ConfigFileConnectionStringsProvider());

            //ASPxDashboard1.ColorScheme = ASPxDashboard.ColorSchemeDarkVioletCompact;

            //ASPxDashboard1.WorkingMode = WorkingMode.ViewerOnly;
        }

        protected void DataLoading(object sender, DataLoadingWebEventArgs e) {
            if(e.DataSourceName == "Object Data Source") {
                e.Data = Invoices.CreateData();
            }
        }
    }
}