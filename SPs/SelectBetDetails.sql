DELIMITER // 

DROP PROCEDURE IF EXISTS SelectBetDetails;

CREATE PROCEDURE SelectBetDetails()
BEGIN
	SELECT trans_id, vendor_member_id, stake, winlost_amount
	FROM Transactions;
END //
 
 DELIMITER ;