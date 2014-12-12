using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// NewsService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class ServiceNews : System.Web.Services.WebService {

    public ServiceNews () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod(Description = "Web method for getting news in one type with the page number.")]
    public DataSet GetSingleCategoryNewsListWithPageNumber(int categoryId, int pageSize, int pageRequest) 
    {
        NewsDAO newsDao = new NewsDAO();
        return newsDao.GetSingleCategoryNewsListWithPageNumber(categoryId, pageSize, pageRequest);
    }
}
