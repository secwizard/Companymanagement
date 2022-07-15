GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SetDefaultTaxForCompany') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SetDefaultTaxForCompany
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SetDefaultTaxForCompany] 
	-- Add the parameters for the stored procedure here
	@companyId as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   --Geting the tax details where default is true--
   declare @defaultCount int=(select count(TaxDetailsId) from TaxDetails where CompanyId=@companyId and IsDefault='1');

   --no row with default true, so set the first one as default--
   if (@defaultCount=0)
   begin
   update TaxDetails set IsDefault='1' where CompanyId=@companyId and TaxDetailsId
   =(select top 1 TaxDetailsId from TaxDetails where CompanyId=@companyId order by TaxDetailsId);
   end
   --if default is more than one row, then keep the last updated default as true and rest defaults will be false--
   else if(@defaultCount>1)
   begin
   update TaxDetails set IsDefault='0' where CompanyId=@companyId and TaxDetailsId
   in 
   (select taxdetailsId from TaxDetails where CompanyId=@companyId and isdefault='1'
   and TaxDetailsId !=(select top 1 TaxDetailsId from TaxDetails where CompanyId=@companyId order by UpdatedOnUTC desc)
   )
   end

END
GO
