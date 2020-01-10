DELIMITER // 

DROP PROCEDURE IF EXISTS InsertBetDetail;

CREATE PROCEDURE InsertBetDetail(
	IN BetDetail JSON
)

BEGIN
	SET @trans_id = JSON_EXTRACT(BetDetail, CONCAT('$.trans_id'));
	SET @vendor_member_id = JSON_EXTRACT(BetDetail, CONCAT('$.vendor_member_id'));
	SET @stake = JSON_EXTRACT(BetDetail, CONCAT('$.stake'));
	SET @winlost_amount = JSON_EXTRACT(BetDetail, CONCAT('$.winlost_amount'));
	SET @val = BetDetail;
	
	INSERT INTO Transactions(trans_id, vendor_member_id, stake, winlost_amount, val) 
	VALUES(@trans_id, @vendor_member_id, @stake, @winlost_amount, @val)
	ON DUPLICATE KEY UPDATE vendor_member_id=@vendor_member_id, stake=@stake, winlost_amount=@winlost_amount, val=@val;
END //
 
 DELIMITER ;