USE [KingITDB]
GO
/****** Object:  UserDefinedFunction [dbo].[getMalls]    Script Date: 20.06.2022 17:38:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER FUNCTION [dbo].[getMalls]
(
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT top(1000)
	mall_id,
	title, 
	(select title from statuses where statuses.status_id = malls.status_id) as status_title,
	halls_count,
	address,
	cost,
	floors_count, 
	value_added_factor
	FROM malls
	where malls.status_id != 3
	order by malls.address, status_title
)
