ALTER TABLE `person`
	ADD COLUMN `enabled` BIT(1) DEFAULT b'1' AFTER `gender`;