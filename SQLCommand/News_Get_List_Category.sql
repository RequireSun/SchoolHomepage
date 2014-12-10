USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[News_Get_List_Category]    Script Date: 12/10/2014 16:18:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[News_Get_List_Category]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[News_Get_List_Category]
GO

USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[News_Get_List_Category]    Script Date: 12/10/2014 16:18:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[News_Get_List_Category]
	-- Add the parameters for the stored procedure here
	@News_Type int, 
	@Page_Size int,
	@Page_Request int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Page_Whole INTEGER,
			@Page_Now INTEGER;
	SET @Page_Whole = @Page_Size * @Page_Request;
	SET @Page_Now = @Page_Whole - @Page_Size;
    
    -- Insert statements for procedure here
    
    SELECT * 
    FROM(
		SELECT ROW_NUMBER() OVER (ORDER BY GETDATE()) row_id, * 
		FROM(
			SELECT TOP (@Page_Whole) * FROM dbo.news WHERE category_id = @News_Type ORDER BY update_time DESC
		)Temp_Row_01
    )Temp_Row_02
    WHERE row_id > @Page_Now
END

GO

