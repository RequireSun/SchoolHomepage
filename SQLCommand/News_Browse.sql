USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[News_Browse]    Script Date: 12/17/2014 15:51:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[News_Browse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[News_Browse]
GO

USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[News_Browse]    Script Date: 12/17/2014 15:51:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[News_Browse]
	-- Add the parameters for the stored procedure here
	@News_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF 1 > @News_ID
	BEGIN
		SELECT * FROM news WHERE id = -1
	END
	ELSE
	BEGIN
		BEGIN TRY
			BEGIN TRANSACTION
			UPDATE news SET page_view = page_view + 1 WHERE @News_ID = id
			COMMIT TRANSACTION
			SELECT * FROM news WHERE id = @News_ID
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
			SELECT * FROM news WHERE id = -1
		END CATCH
	END
END

GO

