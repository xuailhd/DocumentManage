/*
Navicat MySQL Data Transfer

Source Server         : 本机
Source Server Version : 50720
Source Host           : localhost:3306
Source Database       : documentmanage

Target Server Type    : MYSQL
Target Server Version : 50720
File Encoding         : 65001

Date: 2017-11-25 11:36:03
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
  `PassportUrlName` longtext,
  `PassportUrl` longtext,
  `PassportDate` datetime DEFAULT NULL,
  `PassportSignDate` datetime DEFAULT NULL,
  `PassportSignAdress` longtext,
  `PassportType` longtext,
  `PhotoUrl` longtext,
  `PhotoUrlName` longtext,
  `Title` longtext,
  `IDNumber` longtext,
  `IDNumberUrl` longtext,
  `IDNumberUrlName` longtext,
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
  `RoleID` varchar(128) NOT NULL,
  `RoleName` longtext,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of roles
-- ----------------------------

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

-- ----------------------------
-- Table structure for `users`
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `UserID` varchar(128) NOT NULL,
  `Password` longtext,
  `UserName` longtext,
  `UserToken` longtext,
  `LastTime` datetime DEFAULT NULL,
  `CreateUserID` longtext,
  `CreateTime` datetime NOT NULL,
  `ModifyUserID` longtext,
  `ModifyTime` datetime DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of users
-- ----------------------------

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
  `Type` longtext,
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
  `MianDepartment` longtext,
  `MianPersonName` longtext,
  `FromDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `VisitTag` longtext,
  `BeViType` longtext,
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
