USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[News_Calculate_Page_Category]    Script Date: 12/13/2014 09:18:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[News_Calculate_Page_Category]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[News_Calculate_Page_Category]
GO

USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[News_Calculate_Page_Category]    Script Date: 12/13/2014 09:18:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[News_Calculate_Page_Category] 
	-- Add the parameters for the stored procedure here
	@Category_Type INT , 
	@Page_Size INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF 1 > @Category_Type OR 1 > @Page_Size
	BEGIN
		RETURN -1;
	END
	
	DECLARE @Rows_Count REAL ;
	
    -- Insert statements for procedure here
	SELECT @Rows_Count = COUNT(*) FROM news WHERE category_id = @Category_Type
	RETURN CEILING(@Rows_Count / @Page_Size);
END

GO

