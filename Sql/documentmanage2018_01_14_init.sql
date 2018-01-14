/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50720
Source Host           : localhost:3306
Source Database       : documentmanage

Target Server Type    : MYSQL
Target Server Version : 50720
File Encoding         : 65001

Date: 2018-01-14 12:31:35
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `authmodels`
-- ----------------------------
DROP TABLE IF EXISTS `authmodels`;
CREATE TABLE `authmodels` (
  `AuthID` varchar(128) NOT NULL,
  `AuthName` longtext,
  `ParentID` longtext,
  `AuthUrl` longtext,
  `CSSClass` longtext,
  `Target` longtext,
  `OrderNo` int(11) NOT NULL,
  `Type` int(11) NOT NULL,
  PRIMARY KEY (`AuthID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of authmodels
-- ----------------------------
INSERT INTO `authmodels` VALUES ('01000', '系统管理', null, '#/Index/Manage/UserList', 'icon-stethoscope', '_self', '0', '0');
INSERT INTO `authmodels` VALUES ('01001', '档案管理', null, '#/Index/Record', 'icon-stethoscope', '_self', '4', '0');
INSERT INTO `authmodels` VALUES ('01002', '机构管理', null, '#/Index/Org', 'icon-stethoscope', '_self', '2', '0');
INSERT INTO `authmodels` VALUES ('01003', '人员管理', null, '#/Index/Person', 'icon-stethoscope', '_self', '3', '0');
INSERT INTO `authmodels` VALUES ('01004', '综合查询', null, '#/Index/Query', 'icon-stethoscope', '_self', '5', '0');
INSERT INTO `authmodels` VALUES ('FN01001', '编辑机构信息', null, null, null, null, '1', '1');
INSERT INTO `authmodels` VALUES ('FN01002', '删除机构信息', null, null, null, null, '2', '1');
INSERT INTO `authmodels` VALUES ('FN02001', '编辑人员信息', null, null, null, null, '1', '1');
INSERT INTO `authmodels` VALUES ('FN02002', '删除人员信息', null, null, null, null, '2', '1');
INSERT INTO `authmodels` VALUES ('FN03001', '编辑档案信息', null, null, null, null, '1', '1');
INSERT INTO `authmodels` VALUES ('FN03002', '删除档案信息', null, null, null, null, '2', '1');
INSERT INTO `authmodels` VALUES ('LE001', '所有数据权限', null, null, null, null, '1', '2');
INSERT INTO `authmodels` VALUES ('LE002', '自己数据权限', null, null, null, null, '2', '2');

-- ----------------------------
-- Table structure for `authrolemaps`
-- ----------------------------
DROP TABLE IF EXISTS `authrolemaps`;
CREATE TABLE `authrolemaps` (
  `MapID` varchar(128) NOT NULL,
  `AuthID` longtext,
  `RoleID` longtext,
  PRIMARY KEY (`MapID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of authrolemaps
-- ----------------------------
INSERT INTO `authrolemaps` VALUES ('0', '01000', 'admin');
INSERT INTO `authrolemaps` VALUES ('1', '01001', 'admin');
INSERT INTO `authrolemaps` VALUES ('2', '01002', 'admin');
INSERT INTO `authrolemaps` VALUES ('3', '01003', 'admin');
INSERT INTO `authrolemaps` VALUES ('4', '01004', 'admin');
INSERT INTO `authrolemaps` VALUES ('FN01001', 'FN01001', 'admin');
INSERT INTO `authrolemaps` VALUES ('FN01002', 'FN01002', 'admin');
INSERT INTO `authrolemaps` VALUES ('FN02001', 'FN02001', 'admin');
INSERT INTO `authrolemaps` VALUES ('FN02002', 'FN02002', 'admin');
INSERT INTO `authrolemaps` VALUES ('FN03001', 'FN03001', 'admin');
INSERT INTO `authrolemaps` VALUES ('FN03002', 'FN03002', 'admin');
INSERT INTO `authrolemaps` VALUES ('LE001', 'LE001', 'admin');

-- ----------------------------
-- Table structure for `loginlogs`
-- ----------------------------
DROP TABLE IF EXISTS `loginlogs`;
CREATE TABLE `loginlogs` (
  `LogID` varchar(128) NOT NULL,
  `LoginAccount` longtext,
  `LoginName` longtext,
  `LoginTime` longtext,
  PRIMARY KEY (`LogID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of loginlogs
-- ----------------------------

-- ----------------------------
-- Table structure for `orgnazitions`
-- ----------------------------
DROP TABLE IF EXISTS `orgnazitions`;
CREATE TABLE `orgnazitions` (
  `OrgID` varchar(128) NOT NULL,
  `OrgName` longtext,
  `FromType` longtext,
  `ShortNameCN` longtext,
  `OrgNameEN` longtext,
  `ShortNameEN` longtext,
  `Tag` longtext,
  `Level` longtext,
  `Address` longtext,
  `Continent` longtext,
  `Country` longtext,
  `Province` longtext,
  `OrgType` longtext,
  `OrgBack` longtext,
  `OrgInfo` longtext,
  `WorkAddress` longtext,
  `WorkTime` longtext,
  `ContactPerson1` longtext,
  `ContactPerson2` longtext,
  `Tel1` longtext,
  `Tel2` longtext,
  `Email1` longtext,
  `Email2` longtext,
  `Tax` longtext,
  `Remark` longtext,
  `CreateUserID` longtext,
  `CreateTime` datetime NOT NULL,
  `ModifyUserID` longtext,
  `ModifyTime` datetime DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`OrgID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of orgnazitions
-- ----------------------------

-- ----------------------------
-- Table structure for `personinfoes`
-- ----------------------------
DROP TABLE IF EXISTS `personinfoes`;
CREATE TABLE `personinfoes` (
  `PersonID` varchar(128) NOT NULL,
  `FromType` longtext,
  `OrgID` longtext,
  `OrgName` longtext,
  `NameCN` longtext,
  `NameEN` longtext,
  `Tag` longtext,
  `Department` longtext,
  `PassportCode` longtext,
  `PassportDate` datetime DEFAULT NULL,
  `PassportSignDate` datetime DEFAULT NULL,
  `PassportSignAdress` longtext,
  `PassportType` longtext,
  `Title` longtext,
  `IDNumber` longtext,
  `Duty` longtext,
  `Email` longtext,
  `Tel1` longtext,
  `Tel2` longtext,
  `Mobile1` longtext,
  `Mobile2` longtext,
  `ContactAddress` longtext,
  `Sex` longtext,
  `Birth` datetime DEFAULT NULL,
  `Nationality` longtext,
  `Fancy` longtext,
  `Taboo` longtext,
  `RecLevel` longtext,
  `Remark` longtext,
  `CreateUserID` longtext,
  `CreateTime` datetime NOT NULL,
  `ModifyUserID` longtext,
  `ModifyTime` datetime DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`PersonID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of personinfoes
-- ----------------------------

-- ----------------------------
-- Table structure for `roles`
-- ----------------------------
DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles` (
  `ID` varchar(128) NOT NULL,
  `RoleID` varchar(128) NOT NULL,
  `RoleName` longtext,
  `IsSystem` bit(1) NOT NULL,
  `State` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of roles
-- ----------------------------
INSERT INTO `roles` VALUES ('admin', 'admin', '超级管理员', '', '0');

-- ----------------------------
-- Table structure for `userrolemaps`
-- ----------------------------
DROP TABLE IF EXISTS `userrolemaps`;
CREATE TABLE `userrolemaps` (
  `MapID` varchar(128) NOT NULL,
  `UserID` longtext,
  `RoleID` longtext,
  PRIMARY KEY (`MapID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of userrolemaps
-- ----------------------------
INSERT INTO `userrolemaps` VALUES ('1', 'admin', 'admin');
-- ----------------------------
-- Table structure for `users`
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `ID` varchar(128) NOT NULL,
  `UserID` varchar(128) NOT NULL,
  `Password` longtext,
  `UserName` longtext,
  `UserToken` longtext,
  `State` int(11) NOT NULL DEFAULT '0',
  `LastTime` datetime DEFAULT NULL,
  `CreateUserID` longtext,
  `CreateTime` datetime NOT NULL,
  `ModifyUserID` longtext,
  `ModifyTime` datetime DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  `IsSystem` bit(1) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES ('admin', 'admin', 'e10adc3949ba59abbe56e057f20f883e', 'admin', '28931c65f7414cbc90fa7a8d3883b04a', '0', '2018-01-13 22:31:55', null, '2017-11-25 11:41:25', null, null, '0', '');
-- ----------------------------
-- Table structure for `visitdetails`
-- ----------------------------
DROP TABLE IF EXISTS `visitdetails`;
CREATE TABLE `visitdetails` (
  `DetailID` varchar(128) NOT NULL,
  `VisitID` longtext,
  `No` longtext,
  `FromDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `Adress` longtext,
  PRIMARY KEY (`DetailID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of visitdetails
-- ----------------------------

-- ----------------------------
-- Table structure for `visitfiles`
-- ----------------------------
DROP TABLE IF EXISTS `visitfiles`;
CREATE TABLE `visitfiles` (
  `FileID` varchar(128) NOT NULL,
  `FileName` longtext,
  `FileUrl` longtext,
  `Type` int(11) DEFAULT NULL,
  `OutID` longtext,
  PRIMARY KEY (`FileID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of visitfiles
-- ----------------------------

-- ----------------------------
-- Table structure for `visitorgs`
-- ----------------------------
DROP TABLE IF EXISTS `visitorgs`;
CREATE TABLE `visitorgs` (
  `VisitOrgID` varchar(128) NOT NULL,
  `OrgID` longtext,
  `VisitID` longtext,
  `OwenType` int(11) NOT NULL,
  PRIMARY KEY (`VisitOrgID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of visitorgs
-- ----------------------------

-- ----------------------------
-- Table structure for `visitpersons`
-- ----------------------------
DROP TABLE IF EXISTS `visitpersons`;
CREATE TABLE `visitpersons` (
  `VisitPersonID` varchar(128) NOT NULL,
  `PersonID` longtext,
  `VisitID` longtext,
  `OwenType` int(11) NOT NULL,
  `Level` int(11) NOT NULL,
  PRIMARY KEY (`VisitPersonID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of visitpersons
-- ----------------------------

-- ----------------------------
-- Table structure for `visitrecords`
-- ----------------------------
DROP TABLE IF EXISTS `visitrecords`;
CREATE TABLE `visitrecords` (
  `VisitID` varchar(128) NOT NULL,
  `VisitType` longtext,
  `VisitName` longtext,
  `VisitFor` longtext,
  `MainDepartment` longtext,
  `FromDate` datetime DEFAULT NULL,
  `EndDate` datetime DEFAULT NULL,
  `VisType` longtext,
  `FeeType` longtext,
  `PayType` longtext,
  `TakeLevel` longtext,
  `IsLine` longtext,
  `AnsLevel` longtext,
  `Remark` longtext,
  `CreateUserID` longtext,
  `CreateTime` datetime NOT NULL,
  `ModifyUserID` longtext,
  `ModifyTime` datetime DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`VisitID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of visitrecords
-- ----------------------------

-- ----------------------------
-- Table structure for `visittags`
-- ----------------------------
DROP TABLE IF EXISTS `visittags`;
CREATE TABLE `visittags` (
  `VisitTagID` varchar(50) NOT NULL,
  `VisitID` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`VisitTagID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of visittags
-- ----------------------------
