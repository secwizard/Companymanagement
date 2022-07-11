GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SetDefaultCompanyTemplateForCompany') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SetDefaultCompanyTemplateForCompany
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SetDefaultCompanyTemplateForCompany] 
	-- Add the parameters for the stored procedure here
	@companyId as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   --Geting the company template where default is true--
   declare @defaultCount int=(select count(CompanyTemplateId) from CompanyTemplate where CompanyId=@companyId and IsDefault='1');

   --no row with default true, so set the first one as default--
   if (@defaultCount=0)
   begin
   update CompanyTemplate set IsDefault='1' where CompanyId=@companyId and CompanyTemplateId
   =(select top 1 CompanyTemplateId from CompanyTemplate where CompanyId=@companyId order by CompanyTemplateId);
   end
   --if default is more than one row, then keep the last updated default as true and rest defaults will be false--
   else if(@defaultCount>1)
   begin
   update CompanyTemplate set IsDefault='0' where CompanyId=@companyId and CompanyTemplateId
   in 
   (select CompanyTemplateId from CompanyTemplate where CompanyId=@companyId and isdefault='1'
   and CompanyTemplateId !=(select top 1 CompanyTemplateId from CompanyTemplate where CompanyId=@companyId order by UpdatedAt desc)
   )
   end

END
GO
