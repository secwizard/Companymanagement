GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SP_GetWishItemDetails') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SP_GetWishItemDetails
ENDGO
CREATE PROCEDURE [dbo].[SP_GetWishItemDetails]  
(  
 @CompanyId BIGINT,
 @ItemId INT,
 @ItemVariantId INT
)  
AS    
/******  
--Retrive data for ItemVariant and ItemVariantPrice AND ItemDetails Based on ItemId  AND ItemVariantId
--Created By: Kuntal Created On: 18 January 2022  
  
--Modified By     Modified Date Reason                 Changes  
----------------------------------------------------------------------  
  
  
--******/  
--DECLARE @CompanyId BIGINT = 6  
    
BEGIN   
 DECLARE @PriceDate DATETIME=(SELECT GETDATE())  
   
 DECLARE @tempItem Table(ItemId BIGINT, [IsItemLevelPrice] bit)  
 INSERT INTO @tempItem  
 SELECT ItemId, [IsItemLevelPrice]   
 FROM [dbo].[Item] it  
 WHERE it.CompanyId = @CompanyId and IsActive=1 and it.IsDeleted=0   
   
 declare @tempprice TABLE (ItemId BIGINT, ItemVariantId BIGINT, ItemPriceId BIGINT, ItemVariantPriceId BIGINT,   
 ItemMRP DECIMAL(18,2),ItemSalePrice DECIMAL(18,2),ItemTaxPct DECIMAL(18,2), ItemMembrPrice DECIMAL(18,2))  
  
 INSERT INTO @tempprice  
 SELECT   
   ItemId = it.ItemId,  
   ItemVariantId = ivp.ItemVariantId,  
   ItemPriceId=0,  
   ItemVariantPriceId=ISNULL(ivp.ItemVariantPriceId,0),  
   ItemMRP=ISNULL(ivp.MRPrice,0.00),  
   ItemSalePrice=ISNULL(ivp.SalePrice,0.00),  
   ItemTaxPct=ISNULL(ivp.TaxPct,0.00),  
   ItemMembrPrice=ISNULL(ivp.MemberPrice,0.00)  
 FROM [dbo].[ItemVariantPrice] ivp   
 INNER JOIN @tempItem it ON it.ItemId = ivp.ItemId   
 WHERE ivp.PriceStartDate <= @PriceDate   
 AND ivp.PriceEndDate >= @PriceDate  
 AND ivp.IsActive=1  
 AND ISNULL(it.[IsItemLevelPrice],0) = 0  AND it.ItemId=@ItemId AND ivp.ItemVariantId=@ItemVariantId
  
 UNION   
  
 SELECT   
   ItemId = it.ItemId,  
   ItemVariantId = 0,  
   ItemPriceId=ISNULL(i.ItemPriceId,0),  
   ItemVariantPriceId=0,  
   ItemMRP=ISNULL(i.MRPrice,0.00),  
   ItemSalePrice=ISNULL(i.SalePrice,0.00),  
   ItemTaxPct=ISNULL(i.TaxPct,0.00),  
   ItemMembrPrice=ISNULL(i.MemberPrice,0.00)  
 FROM [dbo].[ItemPrice] i   
 INNER JOIN @tempItem it ON it.ItemId = i.ItemId   
 WHERE i.PriceStartDate <= @PriceDate   
 AND i.PriceEndDate >= @PriceDate  
 AND i.IsActive=1  
 AND ISNULL(it.[IsItemLevelPrice],0) = 1  AND it.ItemId=@ItemId 
  
 --select * from @tempItem  
 --select * from @tempprice  
  
 SELECT   
  Id=NEWID(),  
  ItemId = i.ItemId,  
  ItemName = i.[Name],  
  ItemDesc = i.[Description],  
  iv.ItemVariantId,  
  VariantName=ISNULL(iv.name,''),  
  Size = ISNULL((CASE  WHEN iv.size = 'xs' or iv.size = 's' OR iv.size = 'm' OR iv.size = 'l' OR iv.size = 'xl' OR iv.size = 'xxl' OR iv.size = 'xxxl' THEN UPPER(iv.Size)  
        ELSE iv.size END),''),  
  [Weight] = ISNULL(iv.[Weight],''),  
  Color = ISNULL(iv.Color,''),  
  ColorCode = ISNULL(iv.ColorCode,''), -- Added by Bulbuli Ghosh on 26.08.2019  
  Material = ISNULL(iv.Material,''),  
  MRP =convert(decimal(10,2), ISNULL(tp.ItemMRP,0)),  
  SalePrice =convert(decimal(10,2), ISNULL(tp.ItemSalePrice,0)),  
  SavePrice =CASE WHEN ISNULL(tp.ItemMRP,0) > 0 THEN convert(decimal(10,2), ISNULL(tp.ItemMRP,0) - ISNULL(tp.ItemSalePrice,0))  
     ELSE 0 END,  
  SavePricePctg =CASE WHEN ISNULL(tp.ItemMRP,0) > 0 THEN convert(decimal(10,0), (ISNULL(tp.ItemMRP,0) - ISNULL(tp.ItemSalePrice,0))*100/ISNULL(tp.ItemMRP,0))  
     ELSE 0 END,  
  MemberPrice = convert(decimal(10,2),ISNULL(tp.ItemMembrPrice,0)),  
  iv.VariantSortOrderInCategory,  
  pro.[PromoText],  
  [PromoPctg] =convert(decimal(10,2), ISNULL(pro.[PromoPctg],0)),  
  [ForMemberOnly] =convert(bit, ISNULL(pro.[ForMemberOnly],0)),  
  CategoryId = i.CategoryId,  
  SubCategoryId = i.SubCategoryId,  
  [BrandLogoFileName] = brd.[LogoFileName],  
  IsSoldOut=i.IsSoldOut,  
  ItemCode = i.ItemCode  
 FROM [dbo].[Item] i  
 LEFT JOIN [dbo].[ItemVariant] iv ON i.ItemId = iv.ItemId AND iv.IsActive =1 AND iv.IsDeleted = 0   
 INNER JOIN @tempprice tp ON tp.ItemId = i.ItemId AND  (tp.ItemVariantId = iv.ItemVariantId OR tp.ItemVariantId=0)  
 LEFT JOIN [dbo].[ItemVariantPromo] pro ON pro.ItemVariantId=iv.ItemVariantId   
   AND pro.[PromoFromDate] <= @PriceDate AND pro.[PromoToDate] >= @PriceDate AND pro.IsActive =1  
 LEFT JOIN [dbo].[Brand] brd ON brd.[BrandId] = i.[BrandId] AND brd.IsActive =1  
 WHERE   i.CompanyId = @CompanyId AND i.IsActive =1  AND i.ItemId=@ItemId AND iv.ItemVariantId=@ItemVariantId
 ORDER BY i.SortOrderInCategory, tp.ItemSalePrice   
   
END
GO
