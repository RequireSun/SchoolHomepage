USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[Information_Modify]    Script Date: 12/12/2014 16:55:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Information_Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Information_Modify]
GO

USE [SchoolHomepage]
GO

/****** Object:  StoredProcedure [dbo].[Information_Modify]    Script Date: 12/12/2014 16:55:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Information_Modify]
	-- Add the parameters for the stored procedure here
	@Category_Id INT, 
	@Article NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF 1 > @Category_Id
	BEGIN
		RETURN -1;
	END
	
	DECLARE @Update_Result INT
	
    -- Insert statements for procedure here
    
    UPDATE information SET article = @Article WHERE category_id = @Category_Id
    SET @Update_Result = @@ROWCOUNT
    
    IF 0 = @Update_Result
    BEGIN
		INSERT INTO information(article,category_id) VALUES(@Article,@Category_Id) 
		RETURN @Category_Id;
    END
    ELSE
    BEGIN
		RETURN @Category_Id;
    END
END

GO

