GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetTaxDetails') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetTaxDetails
END
GO

CREATE PROCEDURE [dbo].[GetTaxDetails]   
 @CompanyId as int 
 ,@TaxDetailsIds nvarchar(max)
AS  
BEGIN  
 select 
  TaxDetailsId
 ,TaxName
 ,Tax1Name
 ,Tax1Percentage
 ,Tax2Name
 ,Tax2Percentage
 ,Tax3Name
 ,Tax3Percentage
 ,Tax4Name
 ,Tax4Percentage
 ,Tax5Name
 ,Tax5Percentage
 ,IsDefault
 ,Total
 from TaxDetails td
	join TaxName tn on td.CompanyId = tn.CompanyId
	where tn.CompanyId = @companyId and td.TaxDetailsId in (select [value] from Split (',',@TaxDetailsIds))
  
END  
GO
