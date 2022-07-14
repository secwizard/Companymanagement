GO
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'GetZoneId') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE GetZoneId
END
GO
CREATE proc [dbo].[GetZoneId] 
(
@CompanyId bigint,
@PostalCode nvarchar(20),
@CityName nvarchar(50),
@StateName nvarchar(50)
)
as
begin

select ZoneId
from
(
select top 1 ZoneId,
case 
when ([PatternValue]=@PostalCode and [PatternType]=1) then 1 --Single Postal Code
when ((','+[PatternValue]+',') like '%,'+@PostalCode+',%' and [PatternType]=2) then 2 --Multiple Postal Code
when (isnull(FromPostalCode,0)<= cast(@PostalCode as Bigint) and isnull(ToPostalCode,0)>= cast(@PostalCode as Bigint) and [PatternType]=3) then 3 --Postal Code Range
when ([PatternValue]=@CityName and [PatternType]=4) then 4 --Single City
when ((','+[PatternValue]+',') like '%,'+@CityName+',%' and [PatternType]=5) then 5 --Multiple City
when ([PatternValue]=@StateName and [PatternType]=6) then 6 --Single State
when ((','+[PatternValue]+',') like '%,'+@StateName+',%' and [PatternType]=7) then 7 --Multiple State
when (isdefault=1 and [PatternType]=0) then 8 --default
else 9 end as MatchRank 
from ZoneSetting
where [CompanyId]=@CompanyId and [IsActive]=1 
order by MatchRank
) as tbl
end
GO
