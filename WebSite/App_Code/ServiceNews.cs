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

    [WebMethod(Description = "获取某一指定子类的指定页码的新闻列表（类型id，页面大小，请求页码）")]
    public DataSet GetSingleCategoryNewsListWithPageNumber(int categoryId, int pageSize, int pageRequest) 
    {
        NewsDAO newsDao = new NewsDAO();
        return newsDao.GetSingleCategoryNewsListWithPageNumber(categoryId, pageSize, pageRequest);
    }

    [WebMethod(Description = "获取某一指定大类的指定页码的新闻列表（类型id，页面大小，请求页码）")]
    public DataSet GetSingleOutlineNewsListWithPageNumber(int outlineId, int pageSize, int pageRequest)
    {
        NewsDAO newsDao = new NewsDAO();
        return newsDao.GetSingleOutlineNewsListWithPageNumber(outlineId, pageSize, pageRequest);
    }

    [WebMethod(Description = "获取某一子类按输入的页面大小确定的总页数（类型id，页面大小）")]
    public int GetNewsPageCountCategory(int categoryId, int pageSize)
    {
        NewsDAO newsDao = new NewsDAO();
        return newsDao.GetNewsPageCountCategory(categoryId, pageSize);
    }

    [WebMethod(Description = "获取某一大类按输入的页面大小确定的总页数（类型id，页面大小）")]
    public int GetNewsPageCountOutline(int outlineId, int pageSize)
    {
        NewsDAO newsDao = new NewsDAO();
        return newsDao.GetNewsPageCountOutline(outlineId, pageSize);
    }

    [WebMethod(Description = "获取学院信息（信息ID）")]
    public string GetInformation(int informationId)
    {
        InformationDAO informationDao = new InformationDAO();
        return informationDao.GetInformation(informationId);
    }

    [WebMethod(Description = "获取学院信息（信息ID）")]
    public string GetInformationName(int informationId)
    {
        InformationDAO informationDao = new InformationDAO();
        return informationDao.GetInformationName(informationId);
    }

    [WebMethod(Description = "获取新闻详情（新闻ID）")]
    public DataSet GetNewsInfo(int newsID)
    {
        NewsDAO newDao = new NewsDAO();
        return newDao.GetNewsInfo(newsID);
    }

}
