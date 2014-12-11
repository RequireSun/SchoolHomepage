USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[News_Publish]    Script Date: 12/11/2014 14:04:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[News_Publish]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[News_Publish]
GO

USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[News_Publish]    Script Date: 12/11/2014 14:04:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[News_Publish]
	-- Add the parameters for the stored procedure here
	@Category_ID INT , 
	@Supervisor_ID INT ,
	@Title NVARCHAR(50) ,
	@Article NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON ;
	
    DECLARE @News_ID INT ,
			@Category_Type INT ,
			@Publish_Date DATETIME ;
			
	SET @Publish_Date = GETDATE() ;
	
	SELECT @Category_Type = outline_id FROM category WHERE id = @Category_ID
	IF 1 = @Category_Type
	BEGIN
		RETURN -1;
	END
	
    -- Insert statements for procedure here
	BEGIN TRY
		BEGIN TRANSACTION

		INSERT INTO News(category_id, supervisor_id, title, article, update_time, page_view) VALUES(@Category_ID, @Supervisor_ID, @Title, @Article, @Publish_Date,0) ;
		SET @News_ID = @@IDENTITY ;				COMMIT TRANSACTION 
		RETURN @News_ID ;
	END TRY
	BEGIN CATCH		ROLLBACK TRANSACTION ;
		RETURN -1 ;
	END CATCH
END

GO

