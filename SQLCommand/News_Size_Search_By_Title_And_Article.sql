USE [SchoolHomepage]
GO
/****** Object:  StoredProcedure [dbo].[News_Size_Search_By_Title_And_Article]    Script Date: 2014/12/16 21:40:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[News_Size_Search_By_Title_And_Article]
	-- Add the parameters for the stored procedure here
	@Page_Size int,
	@Search_Content nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Row_Count REAL;
    
    -- Insert statements for procedure here

	SELECT @Row_Count=COUNT(*) FROM dbo.news WHERE article LIKE '%'+@Search_Content+'%' OR title LIKE '%'+@Search_Content+'%'
	RETURN CEILING(@Row_Count/@Page_Size)
END
