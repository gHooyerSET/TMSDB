-- MySQL dump 10.13  Distrib 8.0.26, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: group15-tms
-- ------------------------------------------------------
-- Server version	8.0.26

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `carrier`
--

DROP TABLE IF EXISTS `carrier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `carrier` (
  `CarrierID` varchar(45) NOT NULL,
  `City` varchar(45) NOT NULL,
  `FTLA` int NOT NULL,
  `LTLA` int NOT NULL,
  `FTLRate` float NOT NULL,
  `LTLRate` float NOT NULL,
  `ReefCharge` float NOT NULL,
  PRIMARY KEY (`CarrierID`,`City`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carrier`
--

LOCK TABLES `carrier` WRITE;
/*!40000 ALTER TABLE `carrier` DISABLE KEYS */;
INSERT INTO `carrier` VALUES ('Planet Express','Belleville',50,640,5.21,0.3621,0.08),('Planet Express','Hamilton',50,640,5.21,0.3621,0.08),('Planet Express','Oshawa',50,640,5.21,0.3621,0.08),('Planet Express','Ottawa',50,640,5.21,0.3621,0.08),('Planet Express','Windsor',50,640,5.21,0.3621,0.08),('Schooner\'s','Kingston',18,98,5.05,0.3434,0.07),('Schooner\'s','London',18,98,5.05,0.3434,0.07),('Schooner\'s','Toronto',18,98,5.05,0.3434,0.07),('Tillman Transport','Hamilton',18,45,5.11,0.3012,0.09),('Tillman Transport','London',18,45,5.11,0.3012,0.09),('Tillman Transport','Windsor',24,35,5.11,0.3012,0.09),('We Haul','Ottawa',11,0,5.2,0,0.065),('We Haul','Toronto',11,0,5.2,0,0.065);
/*!40000 ALTER TABLE `carrier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `invoice`
--

DROP TABLE IF EXISTS `invoice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `invoice` (
  `InvoiceID` int unsigned NOT NULL AUTO_INCREMENT,
  `CustomerID` varchar(45) NOT NULL,
  `OrderID` int unsigned NOT NULL,
  `Cost` float NOT NULL,
  PRIMARY KEY (`InvoiceID`),
  UNIQUE KEY `InvoiceID_UNIQUE` (`InvoiceID`),
  KEY `CustomerID_idx` (`CustomerID`),
  KEY `InvoiceOrderID_idx` (`OrderID`),
  CONSTRAINT `InvoiceCustomerID` FOREIGN KEY (`CustomerID`) REFERENCES `user` (`UserID`),
  CONSTRAINT `InvoiceOrderID` FOREIGN KEY (`OrderID`) REFERENCES `order` (`OrderID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoice`
--

LOCK TABLES `invoice` WRITE;
/*!40000 ALTER TABLE `invoice` DISABLE KEYS */;
INSERT INTO `invoice` VALUES (1,'buyer1',1,20);
/*!40000 ALTER TABLE `invoice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order` (
  `OrderID` int unsigned NOT NULL AUTO_INCREMENT,
  `CustomerID` varchar(45) NOT NULL,
  `StartCity` varchar(45) DEFAULT NULL,
  `EndCity` varchar(45) DEFAULT NULL,
  `Status` varchar(45) NOT NULL,
  `OrderDate` datetime NOT NULL,
  PRIMARY KEY (`OrderID`),
  UNIQUE KEY `OrderID_UNIQUE` (`OrderID`),
  KEY `CustomerID_idx` (`CustomerID`),
  CONSTRAINT `OrderCustomerID` FOREIGN KEY (`CustomerID`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
INSERT INTO `order` VALUES (1,'buyer1','Hamilton','Windsor','Complete','2021-11-08 00:00:00'),(2,'buyer1','Belleville','Ottawa','Complete','2021-11-08 00:00:00'),(3,'buyer1','Bellville','Toronto','Incomplete','2021-11-18 21:57:14'),(4,'buyer1','Bellville','Toronto','Incomplete','2021-11-18 21:58:50'),(5,'buyer1','Bellville','Toronto','Incomplete','2021-11-18 22:03:06'),(6,'buyer1','Bellville','Toronto','Incomplete','2021-11-18 22:03:37'),(7,'buyer1','Bellville','Toronto','Incomplete','2021-11-18 22:04:54'),(8,'buyer1','Bellville','Toronto','Incomplete','2021-11-20 13:44:47'),(9,'buyer1','Bellville','Toronto','Incomplete','2021-11-20 13:45:20'),(10,'buyer1','Bellville','Toronto','Incomplete','2021-11-20 13:46:50'),(11,'buyer1','Bellville','Toronto','Incomplete','2021-11-20 13:48:08'),(12,'buyer1','Bellville','Toronto','Incomplete','2021-11-20 13:49:06'),(13,'buyer2','Belleville','Toronto','Incomplete','2021-11-08 00:00:00');
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `route`
--

DROP TABLE IF EXISTS `route`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `route` (
  `RouteID` int unsigned NOT NULL AUTO_INCREMENT,
  `OrderID` int unsigned DEFAULT NULL,
  `PlannerID` varchar(45) DEFAULT NULL,
  `RouteStatus` varchar(45) DEFAULT NULL,
  `StartCity` varchar(45) DEFAULT NULL,
  `EndCity` varchar(45) DEFAULT NULL,
  `Cost` float DEFAULT NULL,
  PRIMARY KEY (`RouteID`),
  UNIQUE KEY `RouteID_UNIQUE` (`RouteID`),
  KEY `RouteOrderID_idx` (`OrderID`),
  KEY `RoutePlannerID_idx` (`PlannerID`),
  CONSTRAINT `RouteOrderID` FOREIGN KEY (`OrderID`) REFERENCES `order` (`OrderID`),
  CONSTRAINT `RoutePlannerID` FOREIGN KEY (`PlannerID`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `route`
--

LOCK TABLES `route` WRITE;
/*!40000 ALTER TABLE `route` DISABLE KEYS */;
INSERT INTO `route` VALUES (1,3,'planner1','Incomplete','Bellville','Toronto',0);
/*!40000 ALTER TABLE `route` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `trip`
--

DROP TABLE IF EXISTS `trip`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `trip` (
  `TripID` int unsigned NOT NULL AUTO_INCREMENT,
  `CarrierID` varchar(45) NOT NULL,
  `RouteID` int unsigned DEFAULT NULL,
  `StartCity` varchar(45) DEFAULT NULL,
  `EndCity` varchar(45) DEFAULT NULL,
  `Type` varchar(45) DEFAULT NULL,
  `Rate` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`TripID`),
  UNIQUE KEY `TripID_UNIQUE` (`TripID`),
  KEY `TripRouteID_idx` (`RouteID`),
  KEY `TripCarrierID_idx` (`CarrierID`),
  CONSTRAINT `TripCarrierID` FOREIGN KEY (`CarrierID`) REFERENCES `carrier` (`CarrierID`),
  CONSTRAINT `TripRouteID` FOREIGN KEY (`RouteID`) REFERENCES `route` (`RouteID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trip`
--

LOCK TABLES `trip` WRITE;
/*!40000 ALTER TABLE `trip` DISABLE KEYS */;
INSERT INTO `trip` VALUES (1,'Planet Express',1,'Belleville','Toronto','FTL','5.21');
/*!40000 ALTER TABLE `trip` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `UserID` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `Role` varchar(45) NOT NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `iduser_UNIQUE` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES ('admin','password','Admin'),('buyer1','password','Buyer'),('buyer2','password','Buyer'),('buyer3','password','Buyer'),('planner1','password','Planner'),('planner2','password','Planner'),('planner3','password','Planner'),('planner4','password','Planner');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-12-04 20:12:58
