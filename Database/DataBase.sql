-- MySQL dump 10.13  Distrib 8.0.13, for Win64 (x86_64)
--
-- Host: localhost    Database: attendance_control
-- ------------------------------------------------------
-- Server version	8.0.13

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `absence`
--

DROP TABLE IF EXISTS `absence`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `absence` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `type` int(1) NOT NULL,
  `date` date NOT NULL,
  `school_class_id` int(11) NOT NULL,
  `student_id` int(11) NOT NULL,
  `is_excused` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `student_id` (`student_id`),
  KEY `school_class_id` (`school_class_id`),
  CONSTRAINT `absence_ibfk_1` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`),
  CONSTRAINT `absence_ibfk_2` FOREIGN KEY (`school_class_id`) REFERENCES `school_class` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `absence`
--

LOCK TABLES `absence` WRITE;
/*!40000 ALTER TABLE `absence` DISABLE KEYS */;
/*!40000 ALTER TABLE `absence` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `after_insert_absence` AFTER INSERT ON `absence` FOR EACH ROW if new.type = 0 then
		UPDATE student 
		SET total_absences = total_absences + 1
		WHERE id = new.student_id;
	else 
		UPDATE student
		SET total_delays = total_delays + 1
		WHERE id = new.student_id;
	end if */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `after_update_absence` AFTER UPDATE ON `absence` FOR EACH ROW if OLD.type = 0 AND new.type = 1 then
		UPDATE student 
		SET total_absences = total_absences - 1,
			total_delays = total_delays + 1
		WHERE id = OLD.student_id;
	elseif OLD.type = 1 AND new.type = 0 then
		UPDATE student 
		SET total_absences = total_absences + 1,
			total_delays = total_delays - 1
		WHERE id = OLD.student_id;
	end if */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `after_delete_absence` AFTER DELETE ON `absence` FOR EACH ROW if old.type = 0 then
		UPDATE student 
		SET total_absences = total_absences - 1
		WHERE id = OLD.student_id;
	elseif old.type = 1 then
		UPDATE student 
		SET total_delays = total_delays - 1
		WHERE id = OLD.student_id;		
	end if */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `administrator`
--

DROP TABLE IF EXISTS `administrator`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `administrator` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `admin_name` varchar(32) NOT NULL,
  `password` varchar(32) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `UQ_admin_name` (`admin_name`),
  UNIQUE KEY `UQ_password` (`password`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrator`
--

LOCK TABLES `administrator` WRITE;
/*!40000 ALTER TABLE `administrator` DISABLE KEYS */;
INSERT INTO `administrator` VALUES (1,'21232f297a57a5a743894a0e4a801fc3','21232f297a57a5a743894a0e4a801fc3');
/*!40000 ALTER TABLE `administrator` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `course`
--

DROP TABLE IF EXISTS `course`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `course` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `year` int(1) NOT NULL,
  `cycle_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `cycle_id` (`cycle_id`),
  CONSTRAINT `course_ibfk_1` FOREIGN KEY (`cycle_id`) REFERENCES `cycle` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `course`
--

LOCK TABLES `course` WRITE;
/*!40000 ALTER TABLE `course` DISABLE KEYS */;
INSERT INTO `course` VALUES (1,1,1),(2,2,1),(3,1,2),(4,2,2),(5,1,3),(6,2,3),(7,1,4),(8,2,4),(9,1,5),(10,2,5),(11,1,6),(12,2,6),(13,1,7),(14,2,7);
/*!40000 ALTER TABLE `course` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `course_has_subjects`
--

DROP TABLE IF EXISTS `course_has_subjects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `course_has_subjects` (
  `course_id` int(11) NOT NULL,
  `subject_id` int(11) NOT NULL,
  PRIMARY KEY (`course_id`,`subject_id`),
  KEY `subject_id` (`subject_id`),
  CONSTRAINT `course_has_subjects_ibfk_1` FOREIGN KEY (`course_id`) REFERENCES `course` (`id`),
  CONSTRAINT `course_has_subjects_ibfk_2` FOREIGN KEY (`subject_id`) REFERENCES `subject` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `course_has_subjects`
--

LOCK TABLES `course_has_subjects` WRITE;
/*!40000 ALTER TABLE `course_has_subjects` DISABLE KEYS */;
INSERT INTO `course_has_subjects` VALUES (1,1),(1,2),(1,3),(1,4),(2,5),(2,6),(2,7),(2,8),(2,9),(3,10),(3,11),(3,12),(3,13),(4,14),(4,15),(4,16),(4,17),(4,18),(1,19),(3,19),(1,20),(3,20),(2,21),(4,21),(2,22),(4,22);
/*!40000 ALTER TABLE `course_has_subjects` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cycle`
--

DROP TABLE IF EXISTS `cycle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `cycle` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `shift_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `UQ_cycle_name` (`name`),
  KEY `shift_id` (`shift_id`),
  CONSTRAINT `cycle_ibfk_1` FOREIGN KEY (`shift_id`) REFERENCES `shift` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cycle`
--

LOCK TABLES `cycle` WRITE;
/*!40000 ALTER TABLE `cycle` DISABLE KEYS */;
INSERT INTO `cycle` VALUES (1,'Desarrollo De Aplicaciones Multiplataforma',1),(2,'Administración De Sistemas Informáticos En Red',2),(3,'Administración y Gestión',1),(4,'Comercio y Marketing',2),(5,'Hostelería y Turismo',1),(6,'Artes Gráficas',2),(7,'Edificación y obra civil',2);
/*!40000 ALTER TABLE `cycle` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `before_update_cycle` BEFORE UPDATE ON `cycle` FOR EACH ROW /* 
		Si se cambia el turno de un ciclo, se cancelan todas las clases
		y se borran las relaciones entre alumnos y clases 
	*/
	if old.shift_id != new.shift_id then
		
        UPDATE school_class 
        SET is_current = false
        WHERE course_id IN 
        (
			select co.id FROM course co 
            INNER JOIN cycle cy 
            ON co.cycle_id = cy.id
            AND cy.id = new.id
		);
            
		DELETE FROM school_class_has_students
        WHERE school_class_id IN 
        (
			SELECT sc.id from school_class sc
            INNER JOIN course co 
            ON co.id = sc.course_id
            AND co.cycle_id = new.id
		);
	end if */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `schedule`
--

DROP TABLE IF EXISTS `schedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `schedule` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `start` time NOT NULL,
  `end` time NOT NULL,
  `shift_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `shift_id` (`shift_id`),
  CONSTRAINT `schedule_ibfk_1` FOREIGN KEY (`shift_id`) REFERENCES `shift` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedule`
--

LOCK TABLES `schedule` WRITE;
/*!40000 ALTER TABLE `schedule` DISABLE KEYS */;
INSERT INTO `schedule` VALUES (1,'08:30:00','09:25:00',1),(2,'09:25:00','10:20:00',1),(3,'10:20:00','11:15:00',1),(4,'11:45:00','12:40:00',1),(5,'12:40:00','13:35:00',1),(6,'13:35:00','14:30:00',1),(7,'15:30:00','16:25:00',2),(8,'16:25:00','17:20:00',2),(9,'17:20:00','18:15:00',2),(10,'18:45:00','19:40:00',2),(11,'19:40:00','20:35:00',2),(12,'20:35:00','21:30:00',2);
/*!40000 ALTER TABLE `schedule` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `school_class`
--

DROP TABLE IF EXISTS `school_class`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `school_class` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `course_id` int(11) NOT NULL,
  `subject_id` int(11) NOT NULL,
  `schedule_id` int(11) NOT NULL,
  `day_of_week` int(1) NOT NULL,
  `is_current` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  KEY `course_id` (`course_id`),
  KEY `subject_id` (`subject_id`),
  KEY `schedule_id` (`schedule_id`),
  CONSTRAINT `school_class_ibfk_1` FOREIGN KEY (`course_id`) REFERENCES `course` (`id`),
  CONSTRAINT `school_class_ibfk_2` FOREIGN KEY (`subject_id`) REFERENCES `subject` (`id`),
  CONSTRAINT `school_class_ibfk_3` FOREIGN KEY (`schedule_id`) REFERENCES `schedule` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `school_class`
--

LOCK TABLES `school_class` WRITE;
/*!40000 ALTER TABLE `school_class` DISABLE KEYS */;
/*!40000 ALTER TABLE `school_class` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `after_insert_school_class` AFTER INSERT ON `school_class` FOR EACH ROW call add_students_to_school_class(new.id,new.subject_id,new.course_id) */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `school_class_has_students`
--

DROP TABLE IF EXISTS `school_class_has_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `school_class_has_students` (
  `student_id` int(11) NOT NULL,
  `school_class_id` int(11) NOT NULL,
  PRIMARY KEY (`student_id`,`school_class_id`),
  KEY `school_class_id` (`school_class_id`),
  CONSTRAINT `school_class_has_students_ibfk_1` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`),
  CONSTRAINT `school_class_has_students_ibfk_2` FOREIGN KEY (`school_class_id`) REFERENCES `school_class` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `school_class_has_students`
--

LOCK TABLES `school_class_has_students` WRITE;
/*!40000 ALTER TABLE `school_class_has_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `school_class_has_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shift`
--

DROP TABLE IF EXISTS `shift`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `shift` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shift`
--

LOCK TABLES `shift` WRITE;
/*!40000 ALTER TABLE `shift` DISABLE KEYS */;
INSERT INTO `shift` VALUES (1,'Mañana'),(2,'Tarde');
/*!40000 ALTER TABLE `shift` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `student` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `dni` varchar(9) NOT NULL,
  `firstname` varchar(255) NOT NULL,
  `lastname1` varchar(255) NOT NULL,
  `lastname2` varchar(255) DEFAULT NULL,
  `course_id` int(11) DEFAULT NULL,
  `total_absences` int(11) DEFAULT '0',
  `total_delays` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `UQ_dni` (`dni`),
  KEY `course_id` (`course_id`),
  KEY `lastnameIndex` (`lastname1`),
  CONSTRAINT `student_ibfk_1` FOREIGN KEY (`course_id`) REFERENCES `course` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student`
--

LOCK TABLES `student` WRITE;
/*!40000 ALTER TABLE `student` DISABLE KEYS */;
INSERT INTO `student` VALUES (1,'21458464Q','David','Ruiz','Lopez',2,0,0),(2,'21578245W','Manuel','Gonzalez','Garcia',2,0,0),(3,'41254484Y','Pablo','Fernandez','Perez',2,0,0),(4,'41245745I','Jose','Moreno','Sevilla',4,0,0),(5,'58942145U','Javier','Diaz','Sanchez',4,0,0),(6,'54892415O','Roberto','García','Martinez',1,0,0),(7,'14578951I','Manuel','Perez','Ramirez',3,0,0),(8,'24158967U','Samuel','Gomez','Aguilar',6,0,0),(9,'14879525J','Jose','Placido','Carmona',5,0,0),(10,'24589412E','Alfonso','Sanchez','Fermón',1,0,0),(11,'21684972U','Ricardo','Rivera','Martín',3,0,0),(12,'54128945Y','Manuel','Gonzalez','Fernandez',5,0,0),(13,'58984249U','Juan','Sevilla','Lopez',6,0,0),(14,'54892514I','Jose','García','Castillo',1,0,0),(15,'91847625L','María','Sierra','Martín',4,0,0);
/*!40000 ALTER TABLE `student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student_has_subjects`
--

DROP TABLE IF EXISTS `student_has_subjects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `student_has_subjects` (
  `student_id` int(11) NOT NULL,
  `subject_id` int(11) NOT NULL,
  PRIMARY KEY (`student_id`,`subject_id`),
  KEY `subject_id` (`subject_id`),
  CONSTRAINT `student_has_subjects_ibfk_1` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`),
  CONSTRAINT `student_has_subjects_ibfk_2` FOREIGN KEY (`subject_id`) REFERENCES `subject` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student_has_subjects`
--

LOCK TABLES `student_has_subjects` WRITE;
/*!40000 ALTER TABLE `student_has_subjects` DISABLE KEYS */;
/*!40000 ALTER TABLE `student_has_subjects` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subject`
--

DROP TABLE IF EXISTS `subject`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `subject` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `teacher_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `UQ_subject_name` (`name`),
  KEY `teacher_id` (`teacher_id`),
  CONSTRAINT `subject_ibfk_1` FOREIGN KEY (`teacher_id`) REFERENCES `teacher` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subject`
--

LOCK TABLES `subject` WRITE;
/*!40000 ALTER TABLE `subject` DISABLE KEYS */;
INSERT INTO `subject` VALUES (1,'Entornos de desarrollo',NULL),(2,'Base de datos',NULL),(3,'Programación',NULL),(4,'Sistemas informáticos',NULL),(5,'Programación multimedia y dispositivos móviles',1),(6,'Sistemas de Gestión Empresarial',2),(7,'Acceso a datos',1),(8,'Desarrollo de interfaces',1),(9,'Programación de procesos y servicios',2),(10,'Planificación y administración de redes',NULL),(11,'Implantación de sistemas operativos',NULL),(12,'Fundamentos de hardware',NULL),(13,'Gestión de bases de datos',NULL),(14,'Administración de sistemas operativos',3),(15,'Servicios de red e Internet',3),(16,'Implantación de aplicaciones web',3),(17,'Administración de sistemas gestores de bases de datos',3),(18,'Seguridad y alta disponibilidad',3),(19,'Lenguajes de marcas',NULL),(20,'Formación Y Orientación Laboral',6),(21,'Empresa e iniciativa emprendedora',4),(22,'Inglés',5);
/*!40000 ALTER TABLE `subject` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `before_update_subject` BEFORE UPDATE ON `subject` FOR EACH ROW /* 
		Si una asignatura se queda sin profesor asignado , se cancelan
        las clases
	*/
	if new.teacher_id is null then
		
        UPDATE school_class 
        SET is_current = false
        WHERE course_id IN 
        (
			select co.id FROM course co 
            INNER JOIN course_has_subjects chs 
            ON co.id = chs.course_id
            AND chs.subject_id = new.id
		);
        
	end if */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `teacher`
--

DROP TABLE IF EXISTS `teacher`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `teacher` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `dni` varchar(9) NOT NULL,
  `firstname` varchar(255) NOT NULL,
  `lastname1` varchar(255) NOT NULL,
  `lastname2` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `UQ_dni` (`dni`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teacher`
--

LOCK TABLES `teacher` WRITE;
/*!40000 ALTER TABLE `teacher` DISABLE KEYS */;
INSERT INTO `teacher` VALUES (1,'12345678R','Juan','Fernandez','Gonzalez'),(2,'12345678U','Luis','Suarez','Ramirez'),(3,'45987142O','Maria','Manzano','Lozano'),(4,'16842874Y','Vanesa','Cruz','Martín'),(5,'53189412I','Jose','Flores','Gamez'),(6,'17458951E','Jesus','Martinez','Perez');
/*!40000 ALTER TABLE `teacher` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'attendance_control'
--

--
-- Dumping routines for database 'attendance_control'
--
/*!50003 DROP PROCEDURE IF EXISTS `add_students_to_school_class` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_students_to_school_class`(
		in_school_class_id int,
        in_subject_id int, 
        in_course_id int)
    DETERMINISTIC
BEGIN
	DECLARE finished INTEGER DEFAULT false;
    DECLARE student_id int;
    
    /* 
		Cursor que selecciona cada alumno asociado al curso y a la asignatura de 
		la nueva clase
	*/
    DEClARE cursorStudents CURSOR FOR 
			SELECT s.id FROM student s
            INNER JOIN student_has_subjects shs
            ON s.id = shs.student_id
            AND s.course_id = in_course_id
            AND shs.subject_id = in_subject_id;
	DECLARE CONTINUE HANDLER 
        FOR NOT FOUND SET finished = true;
    
	OPEN cursorStudents;

	loop_students: LOOP
		FETCH cursorStudents INTO student_id;
        
		IF finished = true THEN 
			LEAVE loop_students;
		END IF;
        
        /* Se crea la relacion con la clase para cada alumno */
        INSERT INTO school_class_has_students
        VALUES(student_id,in_school_class_id);
        	
	END LOOP loop_students;
        
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `add_student_to_course` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_student_to_course`(in_student_id int,in_course_id int)
    DETERMINISTIC
BEGIN

	DECLARE finished INTEGER DEFAULT false;
    DECLARE subject_id int;
    DECLARE school_class_id int;
    /* Selecciona cada asignatura asociada al curso*/
    DEClARE cursorSubjects CURSOR FOR 
			SELECT s.id FROM subject s
            INNER JOIN course_has_subjects chs
            ON s.id = chs.subject_id
            AND chs.course_id = in_course_id;
	/* Selecciona cada clase del curso */
    DEClARE cursorSchoolClasses CURSOR FOR 
			SELECT id FROM school_class 
            WHERE course_id = in_course_id;
            
	DECLARE CONTINUE HANDLER 
        FOR NOT FOUND SET finished = true;
      
	DECLARE EXIT HANDLER FOR SQLEXCEPTION 
    BEGIN
		ROLLBACK;
		RESIGNAL;		
    END;
    
    START TRANSACTION;
	
		/* SE ACTUALIZA EL ID DEL CURSO */
		UPDATE student 
		SET course_id = in_course_id
		WHERE id = in_student_id;
	
		/* SE ACTUALIZAN LAS ASIGNATURAS QUE EL ALUMNO 
        TENDRÁ QUE CURSAR */
        
        /* PRIMERO SE BORRAN LAS ANTIGUAS */
        DELETE FROM student_has_subjects
        WHERE student_id = in_student_id;
        
        /* 
			SE INSERTAN LAS RELACIONES ENTRE EL ALUMNO 
			Y las ASIGNATURAS DEL NUEVO CURSO
        */
        OPEN cursorSubjects;

		loop_subjects: LOOP
		FETCH cursorSubjects INTO subject_id;
        
			IF finished = true THEN 
				LEAVE loop_subjects;
			END IF;
			
			INSERT INTO student_has_subjects
			VALUES(in_student_id,subject_id);
        
	
		END LOOP loop_subjects;
        
        
        /* SE ACTUALIZAN LAS CLASES PRESENCIALES DEL 
        ALUMNO */
        
        /* PRIMERO SE BORRAN LAS ANTIGUAS CLASES */
        DELETE FROM school_class_has_students
        WHERE student_id = in_student_id;
        
        /* 
			SE INSERTAN LAS RELACIONES ENTRE EL ALUMNO 
			Y LAS CLASES DEL NUEVO CURSO 
        */
        SET finished = false;/*Nuevo loop, reinicio el booleano*/
        
        OPEN cursorSchoolClasses;

		loop_school_classes: LOOP
		FETCH cursorSchoolClasses INTO school_class_id;
        
			IF finished = true THEN 
				LEAVE loop_school_classes;
			END IF;
			
			INSERT INTO school_class_has_students
			VALUES(in_student_id,school_class_id);
        
	
		END LOOP loop_school_classes;
        
	COMMIT;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `add_subject_to_course` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_subject_to_course`(in_subject_id int, in_course_id int)
    DETERMINISTIC
BEGIN

	DECLARE finished INTEGER DEFAULT false;
    DECLARE student_id int;
    DEClARE cursorStudents CURSOR FOR 
			SELECT id FROM student 
            WHERE course_id = in_course_id;
	DECLARE CONTINUE HANDLER 
        FOR NOT FOUND SET finished = true;
	DECLARE EXIT HANDLER FOR SQLEXCEPTION 
    BEGIN
		ROLLBACK;
		RESIGNAL;		
    END;
    
	START TRANSACTION;
		  
		INSERT INTO course_has_subjects 
		VALUES( in_course_id, in_subject_id);
    
		OPEN cursorStudents;

		loop_students: LOOP
        
			FETCH cursorStudents INTO student_id;
			
			IF finished = true THEN 
				LEAVE loop_students;
			END IF;
			
			INSERT INTO student_has_subjects
			VALUES(student_id,in_subject_id);
        
		END LOOP loop_students;
        
    COMMIT;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `cancel_school_class` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `cancel_school_class`(in_school_class_id int)
    DETERMINISTIC
BEGIN

	DECLARE EXIT HANDLER FOR SQLEXCEPTION 
    BEGIN
		ROLLBACK;
		RESIGNAL;		
    END;
    
    START TRANSACTION;
	
		/* LA CLASE DEJA DE SER VIGENTE */
		UPDATE school_class 
        SET is_current = false
        WHERE id = in_school_class_id;
        
        /* LOS ALUMNOS YA NO PRESENCIAN LA CLASE */
        DELETE FROM school_class_has_students
        WHERE school_class_id = in_school_class_id;
        
	COMMIT;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insert_absence` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insert_absence`(in_school_class_id int,in_student_id int,in_type int)
    DETERMINISTIC
BEGIN

	DECLARE absence_id int;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION 
    BEGIN
		ROLLBACK;
		RESIGNAL;	
    END;
    /* Comprueba si la ausencia ya existe */
    SET absence_id := (
		SELECT id FROM absence 
        WHERE school_class_id = in_school_class_id
        AND student_id = in_student_id
        AND date = current_date()
        );
	/*  Si existe */
    IF absence_id is not null then	
		/* Si la nueva ausencia tiene el tipo 2 "Cancelada", se borra*/
        IF in_type = 2 THEN
			/* Dispara trigger */
			DELETE FROM absence WHERE id = absence_id;
		/* Sino se actualiza el tipo */
        /* Dispara trigger */
        ELSE 
			UPDATE absence SET type = in_type 
			WHERE id = absence_id;
		END IF;
	/* Si no existe, se inserta una nueva  */
    /* Dispara trigger */
	else
		INSERT INTO absence 
        VALUES( default, in_type,
				current_date(),
				in_school_class_id, 
                in_student_id,default);	
    END IF;	
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insert_new_cyle` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insert_new_cyle`(in_name varchar(255),in_shift_id INT,in_courses INT, out out_cycle_id int )
    DETERMINISTIC
BEGIN
	DECLARE finished INTEGER DEFAULT false;
    DECLARE i INT DEFAULT 1; 
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION 
    BEGIN
		ROLLBACK;
		RESIGNAL;		
    END;
    
    START TRANSACTION;
			
        INSERT INTO cycle VALUES(DEFAULT,in_name,in_shift_id);
        set out_cycle_id = last_insert_id();/*Recupero el id del ciclo*/
        
        WHILE i <= in_courses DO
			INSERT INTO course VALUES(default,i,out_cycle_id);
			SET i = i + 1;
		END WHILE;
 
	COMMIT;	
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `remove_student_from_course` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `remove_student_from_course`(in_student_id int)
    DETERMINISTIC
BEGIN

	DECLARE EXIT HANDLER FOR SQLEXCEPTION 
    BEGIN
		ROLLBACK;
		RESIGNAL;		
    END;
      
    START TRANSACTION;
	
		/* SE ACTUALIZA EL ID DEL CURSO A NULO */
		UPDATE student 
		SET course_id = null
		WHERE id = in_student_id;
	
        /* SE BORRAN LAS RELACIONES DEL ALUMNO CON LAS ASIGNATURAS */        
        DELETE FROM student_has_subjects
        WHERE student_id = in_student_id;
        
        /* SE BORRAN LAS RELACIONES DEL ALUMNO CON LAS CLASES */        
        DELETE FROM school_class_has_students
        WHERE student_id = in_student_id;
        
	COMMIT;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `remove_subject_from_course` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `remove_subject_from_course`(in_subject_id int, in_course_id int)
    DETERMINISTIC
BEGIN
	
    DECLARE EXIT HANDLER FOR SQLEXCEPTION 
    BEGIN
		ROLLBACK;
		RESIGNAL;		
    END;
    
	START TRANSACTION;
      
		/* BORRA LA RELACION ENTRE EL CURSO Y LA ASIGNATURA */
		DELETE FROM course_has_subjects 
		WHERE course_id = in_course_id 
		AND subject_id =in_subject_id;
		
        /* BORRA LA RELACION ENTRE LOS ALUMNOS DEL CURDO 
        Y LA ASIGNATURA */		
		DELETE FROM student_has_subjects 
		WHERE student_id in  
			(
				SELECT id FROM 
                (
					SELECT s.id FROM student s
					INNER JOIN student_has_subjects shs
					ON s.id = shs.student_id 
					AND s.course_id = in_course_id
					AND shs.subject_id = in_subject_id 
				)x
			) ;
          
                
		/* LAS CLASES DE LA ASIGNATURA DEJAN DE SER
        VIGENTES */		
		UPDATE school_class SET is_current = false
		WHERE course_id = in_course_id 
		AND subject_id =in_subject_id;
		
		
		/* BORRA LAS RELACIONES ENTRE LOS ALUMNOS Y LAS CLASES */
		DELETE FROM school_class_has_students 
		WHERE school_class_id IN 
			(
				SELECT id FROM
                (
					SELECT id FROM school_class
					WHERE course_id = in_course_id 
					AND subject_id =in_subject_id
				)x
			);
	
    COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `update_student_assigned_subjects` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `update_student_assigned_subjects`(
			in_student_id INT,
            in_subject_ids JSON)
    DETERMINISTIC
BEGIN
	DECLARE finished INTEGER DEFAULT false;
    DECLARE subjectId INT;
    DECLARE courseId INT;
    DECLARE schoolClassId INT;
    DECLARE i INT DEFAULT 0; 
    	
	DECLARE EXIT HANDLER FOR SQLEXCEPTION 
    BEGIN
		ROLLBACK;
		RESIGNAL;		
    END;
    
    START TRANSACTION;
		/* Recupero el id del curso cursado por el alumno */
        SELECT course_id into courseId FROM student WHERE id = in_student_id; 
        DELETE FROM student_has_subjects WHERE student_id = in_student_id;/* BORRA LAS ANTIGUAS ASIGNATURAS DEL ALUMNO */		
        DELETE FROM school_class_has_students WHERE student_id = in_student_id;        /* BORRA LAS ANTIGUAS CLASES DEL ALUMNO*/
        
        WHILE i < JSON_LENGTH(in_subject_ids) DO
			SELECT JSON_EXTRACT(in_subject_ids, concat('$[', i ,']')) into subjectId;
            /* CREA LAS RELACIONES ENTRE EL ALUMNO Y LAS NUEVAS ASIGNATURAS */
			INSERT INTO student_has_subjects VALUES (in_student_id, subjectId );
            
            BLOCK2:BEGIN
				/* Selecciona cada clase vigente del curco y de la asignatura */
				DEClARE cursor_school_classes CURSOR FOR 
					SELECT s.id FROM school_class s
					WHERE s.subject_id = subjectId
					AND s.is_current = true
                    AND s.course_id = courseId;
            
				DECLARE CONTINUE HANDLER FOR NOT FOUND SET finished = true;  
				/*ASIGNA LAS NUEVAS CLASES AL ALUMNO */
				OPEN cursor_school_classes;
				loop_school_classes: LOOP
					
					FETCH cursor_school_classes INTO schoolClassId;
				
					IF finished = true THEN 
						LEAVE loop_school_classes;
					END IF;

					INSERT INTO school_class_has_students
					VALUES(in_student_id,schoolClassId);
                
				END LOOP loop_school_classes;	
                
            END BLOCK2;
            
			SET i = i + 1;
            set finished = false;
            
		END WHILE;
	COMMIT;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-03 19:45:47
