GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SaveDeleteLegendFieldConfig') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SaveDeleteLegendFieldConfig
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SaveDeleteLegendFieldConfig] 
	(
	@CompanyId int,
	@NotificationTypeId int,
	@LegendKey nvarchar(50),
	@FieldName nvarchar(50),
	@CreatedBy uniqueidentifier,
	@CreatedDate datetime,
	@ModifiedBy uniqueidentifier,
	@ModifiedDate datetime,
	@Action int
)
AS
BEGIN

	IF (ISNULL(@Action, 0) = 1)
	BEGIN
	INSERT INTO LegendFieldConfig
							 (CompanyId, NotificationTypeId, LegendKey, FieldName, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
	VALUES        (@CompanyId, @NotificationTypeId, @LegendKey, @FieldName, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy);
	END
	ELSE IF (ISNULL(@Action, 0) = 2)
	BEGIN
	DELETE FROM LegendFieldConfig WHERE [CompanyId]=@CompanyId AND [NotificationTypeId]=@NotificationTypeId 
	AND [LegendKey]=@LegendKey AND [FieldName]=@FieldName;
	END

END
GO
