﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <a href="InformationDetail.aspx?id=1">学院简介</a>
    <a href="InformationDetail.aspx?id=2">机构设置</a>
    <a href="InformationDetail.aspx?id=3">历史沿革</a>
    <a href="InformationDetail.aspx?id=4">校企合作</a>
    <a href="InformationDetail.aspx?id=5">实习实训</a>
    <a href="InformationDetail.aspx?id=6">专著编著</a>
    <a href="InformationDetail.aspx?id=7">科技项目与论文</a>
    <a href="InformationDetail.aspx?id=8">教学论文</a>
    <a href="InformationDetail.aspx?id=10">招生信息</a>
    <a href="InformationDetail.aspx?id=11">学生就业</a>
    <br />
    <a href="NewsList.aspx?type=9&page_request=1">现任领导</a>
    <a href="NewsList.aspx?type=12&page_request=1">师资队伍</a>
    <a href="NewsList.aspx?type=18&page_request=1">教学动态</a>
    <a href="NewsList.aspx?type=13&page_request=1">学科建设</a>
    <a href="NewsList.aspx?type=14&page_request=1">人才培养</a>
    <a href="NewsList.aspx?type=19&page_request=1">就业信息</a>
    <a href="NewsList.aspx?type=20&page_request=1">实习实践</a>
    <a href="NewsList.aspx?type=21&page_request=1">学生思政</a>
    <a href="NewsList.aspx?type=15&page_request=1">学生动态</a>
    <a href="NewsList.aspx?type=16&page_request=1">党团建设</a>
    <a href="NewsList.aspx?type=17&page_request=1">优秀典型</a>
    <br />
    <a href="OutlineList.aspx?type=2&page_request=1">学院新闻</a>
    <ul id="news_list" runat="server"></ul>
    <a href="OutlineList.aspx?type=3&page_request=1">通知公告</a>
    <ul id="notify_list" runat="server"></ul>
</body>
</html>
