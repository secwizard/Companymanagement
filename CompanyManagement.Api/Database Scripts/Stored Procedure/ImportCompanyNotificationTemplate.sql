GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'ImportCompanyNotificationTemplate') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE ImportCompanyNotificationTemplate
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ImportCompanyNotificationTemplate]
(
	@NotificationTemplateMasterId int,
	@CompanyId int,
	@IsActive bit,
	@CreatedBy uniqueidentifier,
	@CreatedDate datetime,
	@ModifiedBy uniqueidentifier,
	@ModifiedDate datetime
)
AS
BEGIN
	INSERT INTO CompanyNotificationTemplate
                         (NotificationTemplateMasterId, NotificationTypeId, EmailTemplate, IsEmailSelect, SMSTemplate, IsSMSSelect, WhatsAppTemplate, IsWhatsAppSelect, RoboCallTemplate, IsRoboCallSelect, IsActive, CreatedDate, CreatedBy, 
                         ModifiedDate, ModifiedBy, CompanyId)
	SELECT NotificationTemplateMasterId, NotificationTypeId, EmailTemplate, IsEmailSelect, SMSTemplate, IsSMSSelect, WhatsAppTemplate, IsWhatsAppSelect, RoboCallTemplate, IsRoboCallSelect, @IsActive, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy, @CompanyId 
	FROM [dbo].[NotificationTemplateMaster] WHERE NotificationTemplateMasterId=@NotificationTemplateMasterId;
END
GO
