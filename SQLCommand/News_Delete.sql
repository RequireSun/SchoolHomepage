USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[News_Delete]    Script Date: 12/12/2014 22:49:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[News_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[News_Delete]
GO

USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[News_Delete]    Script Date: 12/12/2014 22:49:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[News_Delete]
	-- Add the parameters for the stored procedure here
	@News_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF 1 > @News_ID
	BEGIN
		RETURN -1;
	END
	
    -- Insert statements for procedure here
    BEGIN TRY
		BEGIN TRANSACTION
			DELETE FROM news WHERE id = @News_ID
		COMMIT TRANSACTION
		RETURN @News_ID;
    END TRY
    BEGIN CATCH
		ROLLBACK TRANSACTION
		RETURN -1;
    END CATCH
END

GO

