USE [SchoolHomepage]
GO
/****** Object:  StoredProcedure [dbo].[News_Publish]    Script Date: 2014/12/12 16:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[News_Edit]
	-- Add the parameters for the stored procedure here
	@Category_ID INT , 
	@News_ID INT,
	@Title NVARCHAR(50) ,
	@Article NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON ;
	
    DECLARE @Category_Type INT ,
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

		UPDATE news SET category_id=@Category_ID, title=@Title, article=@Article, update_time=@Publish_Date WHERE id=@News_ID				COMMIT TRANSACTION 
		RETURN @News_ID ;
	END TRY
	BEGIN CATCH		ROLLBACK TRANSACTION ;
		RETURN -1 ;
	END CATCH
END
