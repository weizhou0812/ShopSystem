/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80030
 Source Host           : localhost:3306
 Source Schema         : shopsystem

 Target Server Type    : MySQL
 Target Server Version : 80030
 File Encoding         : 65001

 Date: 20/06/2023 16:18:37
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for __efmigrationshistory
-- ----------------------------
DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of __efmigrationshistory
-- ----------------------------
INSERT INTO `__efmigrationshistory` VALUES ('20230620013932_initdb', '7.0.5');

-- ----------------------------
-- Table structure for employees
-- ----------------------------
DROP TABLE IF EXISTS `employees`;
CREATE TABLE `employees`  (
  `ENumber` int(0) NOT NULL AUTO_INCREMENT,
  `EId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ESex` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EAge` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EBirth` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EEmail` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EIdentity` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedDate` datetime(6) NOT NULL,
  `IsOnJob` tinyint(1) NOT NULL,
  PRIMARY KEY (`ENumber`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of employees
-- ----------------------------
INSERT INTO `employees` VALUES (1, '000001', 'admin', '男', '23', '2000-08-12', 'admin@123', '19805288108', '江苏苏州', '超级管理员', '2023-06-19 08:46:16.000000', 1);
INSERT INTO `employees` VALUES (2, '000002', '张三', '女', '45', '1978-03-03', 'zhangsan@123', '17825623956', '上海', '董事长', '2023-06-20 09:50:36.000000', 1);
INSERT INTO `employees` VALUES (4, '000003', '李四', '男', '32', '1991-03-23', 'lisi@123', '13786521456', '江苏无锡', '经理', '2023-06-20 09:53:31.000000', 1);
INSERT INTO `employees` VALUES (5, '000004', '赵武', '男', '25', '1998-06-20', 'zhaowu@123', '13784237913', '江苏南通', '销售', '2023-06-20 10:02:40.000000', 1);
INSERT INTO `employees` VALUES (6, '000005', '吴六甲', '男', '62', '1961-12-12', 'wuliujia@123', '17845218956', '江苏苏州', '保安', '2023-06-20 10:05:37.000000', 1);

-- ----------------------------
-- Table structure for products
-- ----------------------------
DROP TABLE IF EXISTS `products`;
CREATE TABLE `products`  (
  `PNumber` int(0) NOT NULL AUTO_INCREMENT,
  `PId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` double NOT NULL,
  `SellbyDate` int(0) NOT NULL,
  `ProduceDate` datetime(6) NOT NULL,
  `WarehousingDate` datetime(6) NOT NULL,
  `Inventory` int(0) NOT NULL,
  `IsExpire` tinyint(1) NOT NULL,
  PRIMARY KEY (`PNumber`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of products
-- ----------------------------
INSERT INTO `products` VALUES (1, 'd0001', '可口可乐', 3, 12, '2022-06-20 11:02:22.191000', '2022-10-20 11:02:22.191000', 200, 0);
INSERT INTO `products` VALUES (2, 'd0002', '百事可乐', 3, 12, '2022-05-20 11:53:32.955000', '2023-02-20 11:53:32.955000', 20, 1);
INSERT INTO `products` VALUES (3, 'd0003', '青梅绿茶/1L', 4, 12, '2023-05-10 00:00:00.000000', '2023-06-20 15:16:03.000000', 50, 0);

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `UNumber` int(0) NOT NULL AUTO_INCREMENT,
  `UId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UPassword` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UIdentity` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsOnJob` tinyint(1) NOT NULL,
  PRIMARY KEY (`UNumber`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (1, '000001', 'admin', 'admin', '管理员', 1);
INSERT INTO `users` VALUES (2, '000002', 'boss0002', 'boss0002', '老板', 1);
INSERT INTO `users` VALUES (3, '000124', 'zhaozhong0124', 'zhaozhong0124', '售货员', 1);
INSERT INTO `users` VALUES (4, '000121', 'zhangsan0121', 'zhangsan0121', '销售经理', 1);
INSERT INTO `users` VALUES (5, '000135', 'liuliu0135', 'liuliu0135', '仓库管理员', 0);
INSERT INTO `users` VALUES (6, '001234', 'lisi1234', 'lisi1234', '收银员', 1);
INSERT INTO `users` VALUES (7, '002135', 'wangwu2135', 'wangwu2135', '普通销售', 0);
INSERT INTO `users` VALUES (8, '000111', '1', '1', '群众', 1);
INSERT INTO `users` VALUES (9, '22', '22', '222', '222', 1);
INSERT INTO `users` VALUES (10, '323', '1222', '12222', '3333', 0);
INSERT INTO `users` VALUES (15, '000015', '000015', '000015', '销售', 1);
INSERT INTO `users` VALUES (16, '000014', '000014', '000014', '仓管', 0);

SET FOREIGN_KEY_CHECKS = 1;
