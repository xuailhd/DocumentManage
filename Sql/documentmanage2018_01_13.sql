/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50720
Source Host           : localhost:3306
Source Database       : documentmanage

Target Server Type    : MYSQL
Target Server Version : 50720
File Encoding         : 65001

Date: 2018-01-03 21:28:57
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
INSERT INTO `authrolemaps` VALUES ('2c44689249e24021a7aae6fcb3491627', 'LE002', '7c6cb406-869f-4465-8b3d-fe41c78c6c60');
INSERT INTO `authrolemaps` VALUES ('3', '01003', 'admin');
INSERT INTO `authrolemaps` VALUES ('4', '01004', 'admin');
INSERT INTO `authrolemaps` VALUES ('498acd49399e494290dcdfb8a184b0a5', '01004', 'testrole');
INSERT INTO `authrolemaps` VALUES ('6d22f393e8ae45c9b08ab0138a2e09d7', '01004', '7c6cb406-869f-4465-8b3d-fe41c78c6c60');
INSERT INTO `authrolemaps` VALUES ('744658a41ac5403cafb2865cc4b449fb', '01003', '7c6cb406-869f-4465-8b3d-fe41c78c6c60');
INSERT INTO `authrolemaps` VALUES ('74b0bb34ed37405e8aa7c6f66b367150', 'FN01001', '7c6cb406-869f-4465-8b3d-fe41c78c6c60');
INSERT INTO `authrolemaps` VALUES ('7b888f4856ab42c3a223b17acb86c08b', '01003', 'testrole');
INSERT INTO `authrolemaps` VALUES ('7dbb369ba964408483d8e54c8cacf50c', 'FN01002', '7c6cb406-869f-4465-8b3d-fe41c78c6c60');
INSERT INTO `authrolemaps` VALUES ('8378cd8ac2cc4d4ea795ce6c1a35d974', '01002', '7c6cb406-869f-4465-8b3d-fe41c78c6c60');
INSERT INTO `authrolemaps` VALUES ('9042409f69db4d19bf19049cd98274b0', '01002', 'testrole');
INSERT INTO `authrolemaps` VALUES ('9fd8606c31f548b1b83a339c4a7f63a0', 'FN03001', '7c6cb406-869f-4465-8b3d-fe41c78c6c60');
INSERT INTO `authrolemaps` VALUES ('b957efc13bdf4e92b041dfeb06dd7860', '01001', 'testrole');
INSERT INTO `authrolemaps` VALUES ('c88f86182b32420da3b0f1d8b9cc50ee', '01001', '7c6cb406-869f-4465-8b3d-fe41c78c6c60');
INSERT INTO `authrolemaps` VALUES ('da713e5c316e492685f674a085876174', '01004', 'testrole');
INSERT INTO `authrolemaps` VALUES ('ed71e8e68efc43269e193371b8798852', 'FN02001', '7c6cb406-869f-4465-8b3d-fe41c78c6c60');
INSERT INTO `authrolemaps` VALUES ('fc55b28e60b3440eabde9611c2d4136a', '01002', 'testrole');
INSERT INTO `authrolemaps` VALUES ('fcd32b3508af4f7b98671aa1707d0e02', 'FN03002', '7c6cb406-869f-4465-8b3d-fe41c78c6c60');
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
INSERT INTO `orgnazitions` VALUES ('WJ2017112511584481', '测试机构1', '外方', 'csjk', 'test1', null, '2', '国家', '北京话是', '亚洲', '中国', '上海', null, null, null, null, null, '许哥', null, '15712064522', null, null, null, '331600', null, 'admin', '2017-11-25 11:59:51', null, null, '0');
INSERT INTO `orgnazitions` VALUES ('WJ2017112615083084', '测试机构2', '外方', '机构2', 'testorg2', 'to2', '2', '世界', 'asdsd', '亚洲', '中国', '北京', null, null, null, null, null, 'aaaa', null, 'bbbbb', null, null, null, '331600', null, 'admin', '2017-11-26 15:09:12', null, null, '0');
INSERT INTO `orgnazitions` VALUES ('WJ2017112912094355', '34543', '外方', '345345', '34543', null, '1', '国家', '345435', '亚洲', null, null, null, null, null, null, null, '345345', null, '34534534534', null, null, null, '331600', null, 'admin', '2017-11-29 12:10:13', null, null, '0');
INSERT INTO `orgnazitions` VALUES ('WJ2017120122290424', 'dfg', '外方', 'dfg', 'dfg', 'dfg', '', '世界', 'dfgdfgdf', '亚洲', '中国', '上海', '社会团体', 'dfgdf', 'dfgdfg', 'dfgfdg', 'dfgfdg', 'dfgfdg', '34534543', '345345', '345345', 'fgdfgf', 'dfgdfg', '331600', 'dfgfdgdf', 'admin', '2017-12-01 22:29:49', 'admin', '2017-12-08 23:06:28', '0');

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
INSERT INTO `personinfoes` VALUES ('32aac1f76fc54e8fbeab47eeef30c026', '外方', null, null, '周', 'zhou', '1', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 'admin', '2017-11-29 11:45:54', null, null, '0');
INSERT INTO `personinfoes` VALUES ('c94c438b082a4b5c9d3cf8b9fad6452b', '外方', 'WJ2017112511584481', '测试机构1', '测试人员2', 'te2', '2', '1123', '123', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 'admin', '2017-11-26 15:00:27', 'admin', '2017-11-29 11:45:27', '0');
INSERT INTO `personinfoes` VALUES ('WR2017112512014037', '外方', 'WJ2017112511584481', '测试机构1', '测试人员1', 'testperson1', '3', '1111', '1111', '2017-10-31 00:00:00', '2017-11-03 00:00:00', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 'admin', '2017-11-25 12:06:36', 'admin', '2017-11-26 14:56:31', '0');
INSERT INTO `personinfoes` VALUES ('WR2017120122295847', '外方', 'WJ2017120122290424', 'dfg', '苹果', 'apple', '2', '树地方', '345645654656564545', '2017-12-21 00:00:00', '2017-12-04 00:00:00', '上海', '因公', '东方古典', '234323432342323', '东方古典', '234234', '234234', '234234', '2342343', '234234', '234234', '男', '2017-12-12 00:00:00', '23423432', '324234', '234234', '三级', '234234234', 'admin', '2017-12-01 22:38:46', null, null, '0');

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
INSERT INTO `roles` VALUES ('7c6cb406-869f-4465-8b3d-fe41c78c6c60', 'test', '测试角色', '', '0');
INSERT INTO `roles` VALUES ('admin', 'admin', '超级管理员', '', '0');
INSERT INTO `roles` VALUES ('testrole', 'testrole', '测试角色11', '', '1');

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
INSERT INTO `userrolemaps` VALUES ('c8d803be7a824c2c8676fd164e768046', 'test', '7c6cb406-869f-4465-8b3d-fe41c78c6c60');

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
INSERT INTO `users` VALUES ('admin', 'admin', 'e10adc3949ba59abbe56e057f20f883e', 'admin', '326e35f969984dca83cff933540eb184', '0', '2018-01-03 20:43:37', null, '2017-11-25 11:41:25', null, null, '0', '');
INSERT INTO `users` VALUES ('test', 'test', 'e10adc3949ba59abbe56e057f20f883e', '测试账号', '', '0', '2017-12-22 23:17:56', null, '2017-12-07 23:19:55', null, null, '0', '');

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
INSERT INTO `visitdetails` VALUES ('74da164fe5104507913cf7de20add10d', 'L2017112922582986', '1', '2017-11-21 08:00:00', '2017-11-22 08:00:00', '北京');
INSERT INTO `visitdetails` VALUES ('954f6af54ff2403fb2f4a51c26411fa3', 'L2017112922582986', '2', '2017-11-23 08:00:00', '2017-11-25 08:00:00', '擦');
INSERT INTO `visitdetails` VALUES ('bf0018f224ac4354a06de17acc249ec6', 'C2017120122511549', '1', '2017-12-01 08:00:00', '2017-12-04 08:00:00', '法国');
INSERT INTO `visitdetails` VALUES ('fd00e237f0c240a8af7cade72019b4ed', 'C2017120122511549', '3', '2017-12-04 08:00:00', '2017-12-05 08:00:00', '中国');
INSERT INTO `visitdetails` VALUES ('ff2ae7b391814bd7b3e03be4c4660ec8', 'C2017120122511549', '2', '2017-12-04 08:00:00', '2017-12-05 08:00:00', '美');

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
INSERT INTO `visitfiles` VALUES ('09cfb6709dd84cd7a5c8de8c059c0be1', 'jqhd.jpg', '201712\\130dae9e79b340118829bd6000023b88.jpg', '12', 'C2017120122511549');
INSERT INTO `visitfiles` VALUES ('11bef80cf3ad45949947b0605fac4d23', 'banner3.jpg', '201711\\b5ee040903564548bdbff3f3a0393edc.jpg', '2', 'WR2017112512014037');
INSERT INTO `visitfiles` VALUES ('1b2998b50e0e469a8e0c14011d6493f2', 'banner1.jpg', '201711\\cee053dfb52643d68fd263a5da669d41.jpg', '1', 'c94c438b082a4b5c9d3cf8b9fad6452b');
INSERT INTO `visitfiles` VALUES ('1e4fcfc4589a4831a887ab138045308f', 'banner_user_packages.png', '201711\\ca2615543ea14194a4eb5db94dd84c2a.png', '16', 'L2017112922582986');
INSERT INTO `visitfiles` VALUES ('2641753d0ee0448ca47930837b0acc2b', 'mmexport1499347867405.jpg', '201712\\17e60ead36f04d9aa642aadd9858cfd2.jpg', '2', 'WR2017120122295847');
INSERT INTO `visitfiles` VALUES ('2817b8f3545f42a5920496f92fe23863', 'banner3.jpg', '201711\\25e75049928c4790991b241198f2f4d3.jpg', '15', 'L2017112922582986');
INSERT INTO `visitfiles` VALUES ('307fd8d71d2a43ddb1f362d7830a3f4f', 'jqhd.jpg', '201712\\fb18dac43eeb451abfe176fa5a6e0533.jpg', '1', 'WR2017120122295847');
INSERT INTO `visitfiles` VALUES ('3acc16286acf4c819bee90cefa471373', 'jqhd.jpg', '201712\\5815ff5b07994264aa991971372c1f45.jpg', '16', 'C2017120122511549');
INSERT INTO `visitfiles` VALUES ('3d1ce20b3cff45b4b797c139db77c075', 'index3.html', '201712\\2ee46cc1cf6e438996c80e84530f7c7e.html', '13', 'C2017120122511549');
INSERT INTO `visitfiles` VALUES ('3dfc9a46804a413590c55f0ebd70fcec', 'banner2.jpg', '201711\\205f5d307f954626addd7bad3c0a48cf.jpg', '11', 'L2017112922582986');
INSERT INTO `visitfiles` VALUES ('42a6bd5aa6a64a04a5d32b601354a5ae', 'banner4.jpg', '201711\\764eecbe997846998130a3c5e48f9468.jpg', '2', 'WR2017112512014037');
INSERT INTO `visitfiles` VALUES ('477baf7ef2e348469e6d3ae52f16af2d', 'jqhd.jpg', '201712\\f260343ee76c4c93937660206eb05bb9.jpg', '11', 'C2017120122511549');
INSERT INTO `visitfiles` VALUES ('5536ff37b45a4ee39d2767fd6e115c39', 'jqhd.jpg', '201712\\0ec644c9b76040fdb70acc4a4927cf9d.jpg', '15', 'C2017120122511549');
INSERT INTO `visitfiles` VALUES ('5b67eba3f1ef4a6ba0a84e49a9d5f23b', 'banner4.jpg', '201711\\612a34881de94c4898c0b0a3e0ac5b4d.jpg', '3', 'c94c438b082a4b5c9d3cf8b9fad6452b');
INSERT INTO `visitfiles` VALUES ('7400115c3f5d447fa3eae7a516dda212', 'mmexport1499347867405.jpg', '201712\\568d2f29037647a48bf8c73571b1ac60.jpg', '14', 'C2017120122511549');
INSERT INTO `visitfiles` VALUES ('7b95fe63650a4b909ff386f57bb4806a', 'banner2.jpg', '201711\\ab35488409fa4f7999c951ea0feae9d9.jpg', '1', 'WR2017112512014037');
INSERT INTO `visitfiles` VALUES ('9b36b56c48f0499a8673b8add47ecfe6', 'banner4.jpg', '201711\\1d4da263f9434af6980e815c32ca2b26.jpg', '15', 'L2017112922582986');
INSERT INTO `visitfiles` VALUES ('a3023cb612ee4c31b628fcc50ad886ae', 'banner3.jpg', '201711\\d7e65927fe9e4eeb8a0e958a624ff742.jpg', '2', 'c94c438b082a4b5c9d3cf8b9fad6452b');
INSERT INTO `visitfiles` VALUES ('af7800db5a17479c9a0f9cffa5692eae', 'banner3.jpg', '201711\\5306346ffe38430a9f1f7fac68be8c41.jpg', '13', 'L2017112922582986');
INSERT INTO `visitfiles` VALUES ('b4e8883eeefe4b14972d6da001b58039', 'banner1.jpg', '201711\\fa9c59ae29fd4b0fa4bd08c121eae6d8.jpg', '11', 'L2017112922582986');
INSERT INTO `visitfiles` VALUES ('cefc2e1a8ac44bc593c8980e5a2805d9', 'banner2.jpg', '201711\\9cae8a49e0bf4ba7a2409c9ae05d6c00.jpg', '2', 'WR2017112512014037');
INSERT INTO `visitfiles` VALUES ('d68bb56418e94ff38a21adc6dd9c2cb0', 'banner4.jpg', '201711\\900603b10f6840ac8c027f92461fbe6c.jpg', '14', 'L2017112922582986');
INSERT INTO `visitfiles` VALUES ('dfcee7478db344249d82744d6c5f3bd6', 'index.jpg', '201712\\a5f9fec7ffa24d8aa867a467b97495b8.jpg', '3', 'WR2017120122295847');
INSERT INTO `visitfiles` VALUES ('e5a023de028046458cf4a8a1069eed89', 'jqhd.jpg', '201712\\246a866645fc4599b97cfe9ae45b0193.jpg', '13', 'C2017120122511549');
INSERT INTO `visitfiles` VALUES ('f4ae58e05e054f92a625f4c04f6f0a90', 'banner2.jpg', '201711\\4474e2481e664c44893b66c16cf9cc6d.jpg', '12', 'L2017112922582986');

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
INSERT INTO `visitorgs` VALUES ('2af069cf01154112beebc24ee805d290', 'WJ2017120122290424', 'C2017120122511549', '1');
INSERT INTO `visitorgs` VALUES ('47350d4b7cd84f9b8b3680cb5d5b14dd', 'WJ2017112615083084', 'L2017112922582986', '1');
INSERT INTO `visitorgs` VALUES ('67a6785fbc4a4bd294d016330d2d1717', 'WJ2017112912094355', 'L2017112922582986', '3');
INSERT INTO `visitorgs` VALUES ('6d0b6ec555024ddba55f3aa48506030a', 'WJ2017112511584481', 'L2017112922582986', '2');
INSERT INTO `visitorgs` VALUES ('740736fdd2b44f1f87323cb29dd121b8', 'WJ2017112912094355', 'L2017112922582986', '3');
INSERT INTO `visitorgs` VALUES ('ac582be2e2dd4fa598b8aa63fc3a2a68', 'WJ2017112615083084', 'C2017120122511549', '2');
INSERT INTO `visitorgs` VALUES ('bc673553447c4ff6b3baeec98c0a3c58', 'WJ2017112912094355', 'L2017112922582986', '3');
INSERT INTO `visitorgs` VALUES ('e642144bc1b24fcd9174503c49e3c982', 'WJ2017112912094355', 'C2017120122511549', '2');
INSERT INTO `visitorgs` VALUES ('f3225340e05e4f099d08ed8cc970f274', 'WJ2017112912094355', 'L2017112922582986', '3');

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
INSERT INTO `visitpersons` VALUES ('007017f43f0d42b2afadc27a3bf45785', 'WR2017112512014037', 'L2017112922582986', '2', '1');
INSERT INTO `visitpersons` VALUES ('24578a4472b74d90a1133061b96c060e', 'WR2017112512014037', 'C2017120122511549', '1', '1');
INSERT INTO `visitpersons` VALUES ('2514b55d42344edaa6092e0e74bc1986', '32aac1f76fc54e8fbeab47eeef30c026', 'L2017112922582986', '0', '0');
INSERT INTO `visitpersons` VALUES ('3cad101f51514822badaf0630b989f96', 'WR2017120122295847', 'C2017120122511549', '0', '0');
INSERT INTO `visitpersons` VALUES ('49b1779819024115a70210f0399f0456', 'WR2017120122295847', 'C2017120122511549', '1', '1');
INSERT INTO `visitpersons` VALUES ('6734572e28024a4d8df679a91938de41', '32aac1f76fc54e8fbeab47eeef30c026', 'C2017120122511549', '1', '0');
INSERT INTO `visitpersons` VALUES ('699de4ccf26e4ba193a0021630a30b7a', 'c94c438b082a4b5c9d3cf8b9fad6452b', 'L2017112922582986', '2', '0');
INSERT INTO `visitpersons` VALUES ('888f0d02c5124896995f1dd5d7479920', 'WR2017120122295847', 'C2017120122511549', '2', '0');
INSERT INTO `visitpersons` VALUES ('8bd5a9315d3640c7b4c5d6569179375e', 'WR2017112512014037', 'L2017112922582986', '1', '1');
INSERT INTO `visitpersons` VALUES ('b0fa23a24db24445896c0cfa8246b696', '32aac1f76fc54e8fbeab47eeef30c026', 'C2017120122511549', '1', '1');
INSERT INTO `visitpersons` VALUES ('cc5d99d6dd2d47c8ae2359346710edef', 'c94c438b082a4b5c9d3cf8b9fad6452b', 'C2017120122511549', '2', '1');
INSERT INTO `visitpersons` VALUES ('cc87a5e63c284cdfbf1f4ddf06342ff1', 'c94c438b082a4b5c9d3cf8b9fad6452b', 'L2017112922582986', '1', '0');
INSERT INTO `visitpersons` VALUES ('fe1713b97ea5450bab74d8e480762ed9', 'c94c438b082a4b5c9d3cf8b9fad6452b', 'C2017120122511549', '1', '1');

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
INSERT INTO `visitrecords` VALUES ('C2017120122511549', '出访', '东方古典发', '会议', '海联', '2017-12-01 00:00:00', '2017-12-05 08:00:00', '散客', '公费', '报销', '二级', '否', '一星', '发鬼画符个', 'admin', '2017-12-01 22:54:33', 'admin', '2017-12-01 22:57:47', '0');
INSERT INTO `visitrecords` VALUES ('L2017112922582986', '来访', '档案名称', '友好访问', '海联', '2017-11-21 00:00:00', '2017-11-25 08:00:00', '散客', '自费', '预付', '二级', '是', '二星', null, 'admin', '2017-11-29 23:02:10', 'admin', '2017-12-01 01:00:53', '0');

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
INSERT INTO `visittags` VALUES ('8efec72d711b41a6bb2f62e66b9fbd12', 'C2017120122511549', '统战部');
INSERT INTO `visittags` VALUES ('b62bf6411f87450a802bdbc8b25c4aac', 'L2017112922582986', '全国两会');
INSERT INTO `visittags` VALUES ('d5a17963b01c4735ba8806bd53d9c76e', 'L2017112922582986', '统战部');
