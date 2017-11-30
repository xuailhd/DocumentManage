/*
Navicat MySQL Data Transfer

Source Server         : 本机
Source Server Version : 50720
Source Host           : localhost:3306
Source Database       : documentmanage

Target Server Type    : MYSQL
Target Server Version : 50720
File Encoding         : 65001

Date: 2017-11-29 22:59:52
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
INSERT INTO `authmodels` VALUES ('01001', '档案管理', null, '#/Index/Record', 'icon-stethoscope', '_self', '4', '0');
INSERT INTO `authmodels` VALUES ('01002', '机构管理', null, '#/Index/Org', 'icon-stethoscope', '_self', '2', '0');
INSERT INTO `authmodels` VALUES ('01003', '人员管理', null, '#/Index/Person', 'icon-stethoscope', '_self', '3', '0');
INSERT INTO `authmodels` VALUES ('01004', '综合查询', null, '#/Index/Query', 'icon-stethoscope', '_self', '5', '0');

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
INSERT INTO `authrolemaps` VALUES ('1', '01001', 'admin');
INSERT INTO `authrolemaps` VALUES ('2', '01002', 'admin');
INSERT INTO `authrolemaps` VALUES ('3', '01003', 'admin');
INSERT INTO `authrolemaps` VALUES ('4', '01004', 'admin');

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
INSERT INTO `orgnazitions` VALUES ('WJ2017112511584481', '测试机构1', '外方', 'csjk', 'test1', null, '2', '国家', '北京话是', '亚洲', '中国', '上海', null, null, null, null, null, '许哥', null, '15712064522', null, null, null, null, 'admin', '2017-11-25 11:59:51', null, null, '0');
INSERT INTO `orgnazitions` VALUES ('WJ2017112615083084', '测试机构2', '外方', '机构2', 'testorg2', 'to2', '2', '世界', 'asdsd', '亚洲', '中国', '北京', null, null, null, null, null, 'aaaa', null, 'bbbbb', null, null, null, null, 'admin', '2017-11-26 15:09:12', null, null, '0');
INSERT INTO `orgnazitions` VALUES ('WJ2017112912094355', '34543', '外方', '345345', '34543', null, '1', '国家', '345435', '亚洲', null, null, null, null, null, null, null, '345345', null, '34534534534', null, null, null, null, 'admin', '2017-11-29 12:10:13', null, null, '0');

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
INSERT INTO `personinfoes` VALUES ('32aac1f76fc54e8fbeab47eeef30c026', '外方', null, null, '周', 'zhou', '1', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 'admin', '2017-11-29 11:45:54', null, null, '0');
INSERT INTO `personinfoes` VALUES ('c94c438b082a4b5c9d3cf8b9fad6452b', '外方', 'WJ2017112511584481', '测试机构1', '测试人员2', 'te2', '2', '1123', '123', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 'admin', '2017-11-26 15:00:27', 'admin', '2017-11-29 11:45:27', '0');
INSERT INTO `personinfoes` VALUES ('WR2017112512014037', '外方', 'WJ2017112511584481', '测试机构1', '测试人员1', 'testperson1', '3', '1111', '1111', 'banner_user_packages.jpg', '201711\\f12970f156254b1fb05e0c2562dc5e9f.jpg', '2017-10-31 00:00:00', '2017-11-03 00:00:00', null, null, '201711\\a64bed72dad649109a0f0133eceaacf9.jpg', 'banner1.jpg', null, null, '201711\\ae62f4c7d0d94e768018fef25cd8369d.jpg', 'banner2.jpg', null, null, null, null, null, null, null, null, null, null, null, null, null, null, 'admin', '2017-11-25 12:06:36', 'admin', '2017-11-26 14:56:31', '0');

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
INSERT INTO `roles` VALUES ('admin', 'admin');

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
INSERT INTO `users` VALUES ('admin', 'e10adc3949ba59abbe56e057f20f883e', 'admin', 'bc904c2d335c435d80ba092273e82ad6', '2017-11-29 20:41:00', null, '2017-11-25 11:41:25', null, null, '0');

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
INSERT INTO `visitfiles` VALUES ('11bef80cf3ad45949947b0605fac4d23', 'banner3.jpg', '201711\\b5ee040903564548bdbff3f3a0393edc.jpg', '2', 'WR2017112512014037');
INSERT INTO `visitfiles` VALUES ('1b2998b50e0e469a8e0c14011d6493f2', 'banner1.jpg', '201711\\cee053dfb52643d68fd263a5da669d41.jpg', '1', 'c94c438b082a4b5c9d3cf8b9fad6452b');
INSERT INTO `visitfiles` VALUES ('42a6bd5aa6a64a04a5d32b601354a5ae', 'banner4.jpg', '201711\\764eecbe997846998130a3c5e48f9468.jpg', '2', 'WR2017112512014037');
INSERT INTO `visitfiles` VALUES ('5b67eba3f1ef4a6ba0a84e49a9d5f23b', 'banner4.jpg', '201711\\612a34881de94c4898c0b0a3e0ac5b4d.jpg', '3', 'c94c438b082a4b5c9d3cf8b9fad6452b');
INSERT INTO `visitfiles` VALUES ('7b95fe63650a4b909ff386f57bb4806a', 'banner2.jpg', '201711\\ab35488409fa4f7999c951ea0feae9d9.jpg', '1', 'WR2017112512014037');
INSERT INTO `visitfiles` VALUES ('a3023cb612ee4c31b628fcc50ad886ae', 'banner3.jpg', '201711\\d7e65927fe9e4eeb8a0e958a624ff742.jpg', '2', 'c94c438b082a4b5c9d3cf8b9fad6452b');
INSERT INTO `visitfiles` VALUES ('cefc2e1a8ac44bc593c8980e5a2805d9', 'banner2.jpg', '201711\\9cae8a49e0bf4ba7a2409c9ae05d6c00.jpg', '2', 'WR2017112512014037');

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
  `FromDate` datetime DEFAULT NULL,
  `EndDate` datetime DEFAULT NULL,
  `VisitTag` longtext,
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
