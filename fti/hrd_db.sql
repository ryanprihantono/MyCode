-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Jul 30, 2012 at 01:48 AM
-- Server version: 5.5.16
-- PHP Version: 5.3.8

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `hrd_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `cancel_request`
--

CREATE TABLE IF NOT EXISTS `cancel_request` (
  `id_cancel_request` int(11) NOT NULL AUTO_INCREMENT,
  `id_process` int(11) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `remarks` text,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id_cancel_request`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `cancel_request`
--

INSERT INTO `cancel_request` (`id_cancel_request`, `id_process`, `register_date`, `remarks`, `status`) VALUES
(1, 1, '2012-07-02', '<p>gkgfhk</p>', NULL),
(2, 1, '2012-07-04', '<p>zxzcz</p>', NULL),
(3, 2, '2012-07-04', '<p>wqeqewqe</p>', NULL),
(4, 2, '2012-07-06', '<p>ytuytuyt</p>', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `company`
--

CREATE TABLE IF NOT EXISTS `company` (
  `id_company` int(11) NOT NULL AUTO_INCREMENT,
  `company` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_company`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Dumping data for table `company`
--

INSERT INTO `company` (`id_company`, `company`) VALUES
(1, 'PT JJ Business Serv (I)'),
(2, 'PT JJ Chemicals Indonesia'),
(3, 'PT JJ Communications Ind'),
(4, 'PT JJ Technology ID'),
(5, 'PT JJ-Lapp Cable SMI'),
(6, 'PT MHE-Demag Indonesia');

-- --------------------------------------------------------

--
-- Table structure for table `completed`
--

CREATE TABLE IF NOT EXISTS `completed` (
  `id_completed` int(11) NOT NULL AUTO_INCREMENT,
  `id_process` int(11) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `remarks` text,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id_completed`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `completed`
--

INSERT INTO `completed` (`id_completed`, `id_process`, `register_date`, `remarks`, `status`) VALUES
(1, 1, '2012-07-11', '<p>fyghdh</p>', NULL),
(2, 1, '2012-07-18', '<p>hsjkjk</p>', NULL),
(3, 2, '2012-07-09', '<p>jhjhjh</p>', NULL),
(4, 2, '2012-07-08', '<p>nbvcnbnbv</p>', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `current_industry`
--

CREATE TABLE IF NOT EXISTS `current_industry` (
  `id_current_industry` int(11) NOT NULL AUTO_INCREMENT,
  `current_industry` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_current_industry`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=30 ;

--
-- Dumping data for table `current_industry`
--

INSERT INTO `current_industry` (`id_current_industry`, `current_industry`) VALUES
(1, 'Accounting/ Audit/ Tax Services'),
(2, 'Agricultural/ Plantation/ Poultry/ Fisheries'),
(3, 'Automobile/ Automotive/ Vehicle'),
(4, 'Bio Technology/ Pharmaceutical'),
(5, 'Call Center/ IT – Enabled Services'),
(6, 'Chemicals/ Fertilizers/ Pesticides'),
(7, 'Computer/ IT – Hardware'),
(8, 'Computer/ IT – Software'),
(9, 'Construction/ Building/ Engineering'),
(10, 'Consumer Products/ FMCG'),
(11, 'Electrical & Electronics'),
(12, 'Environment/ Health/ Safety'),
(13, 'General & Wholesale Trading'),
(14, 'Heavy Industrial/ Machinery/ Equipment'),
(15, 'Human Resources Practitioner/ Consulting'),
(16, 'Manufacturing/ Production'),
(17, 'Marine/ Aquaculture'),
(18, 'Mining'),
(19, 'Oil/ Gas/ Petroleum'),
(21, 'Power Plant'),
(22, 'Repair & Maintenance Service'),
(23, 'Retail/ Merchandise'),
(24, 'Science & Technology'),
(25, 'Semiconductor'),
(26, 'Telecommunication'),
(27, 'Transportation/ Logistics'),
(28, 'Wood/ Fiber/ Paper'),
(29, 'Other');

-- --------------------------------------------------------

--
-- Table structure for table `cv_accepted`
--

CREATE TABLE IF NOT EXISTS `cv_accepted` (
  `id_cv_accepted` int(11) NOT NULL AUTO_INCREMENT,
  `id_process` int(11) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `remarks` text,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id_cv_accepted`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `cv_accepted`
--

INSERT INTO `cv_accepted` (`id_cv_accepted`, `id_process`, `register_date`, `remarks`, `status`) VALUES
(1, 1, '2012-07-06', '<p>aha</p>', NULL),
(2, 1, '2012-07-15', '<p>ihi</p>', NULL),
(3, 2, '2012-07-24', '<p>asiasik</p>', NULL),
(4, 2, '2012-06-15', '<p>gogogo</p>', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `employee`
--

CREATE TABLE IF NOT EXISTS `employee` (
  `id_employee` char(7) NOT NULL,
  `nm_employee` varchar(255) NOT NULL,
  `id_position` int(11) NOT NULL,
  `location` varchar(255) NOT NULL,
  `company_code` int(11) NOT NULL,
  `approver_1` char(7) NOT NULL,
  `approver_2` char(7) NOT NULL,
  `approver_3` char(7) NOT NULL,
  `password` varchar(100) NOT NULL,
  `status` enum('HRD','User') NOT NULL,
  PRIMARY KEY (`id_employee`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `employee`
--

INSERT INTO `employee` (`id_employee`, `nm_employee`, `id_position`, `location`, `company_code`, `approver_1`, `approver_2`, `approver_3`, `password`, `status`) VALUES
('11111', 'Vivi', 0, '', 0, '', '', '', '21232f297a57a5a743894a0e4a801fc3', 'HRD'),
('40066', 'Dede M Sulaeman', 29, '', 4, '40786', '', '', '28b6e3abcd6d9008d1fd710ce64ec827', 'User'),
('40074', 'Bambang Edhy Surjoputro', 21, '', 4, '40786', '', '', 'ac5763e3afcec01a343d31d1c6724a6d', 'User'),
('40130', 'Gunawan Hartanto', 20, '', 4, '40786', '', '', 'bf440032bc90a1af197dd21c81fd5b37', 'User'),
('40160', 'Ibnu Rusyd Elwahby', 21, '', 4, '40786', '', '', 'fcd6ee20a31612b7f2f886ed16348750', 'User'),
('40334', 'Hardianto Rahardjo', 54, '', 6, '41053', '', '', '93ef614de3d8b9d2c590399c13ba8ca5', 'User'),
('40349', 'Ferry Gunawan', 36, '', 6, '40751', '41053', '', 'beeb6c4c36641f6c141372fb4410d558', 'User'),
('40350', 'Sugiyono', 33, '', 6, '40751', '41053', '', '06f2ae7068ee80cf19e8083bf7f3091e', 'User'),
('40355', 'Endarto Isworo Hadjar Darujati', 40, '', 6, '40751', '41053', '', '6790fe5f90c88ea86e251454dd2b8855', 'User'),
('40360', 'Jalontar Lumbantoruan', 40, '', 6, '40751', '41053', '', '66fe0ba05151fe9dad7eae95fb9144e1', 'User'),
('40363', 'Rusmawati Sinaga', 32, '', 6, '40751', '41053', '', '0113c64ca9d9475255c5d0d83e6d2367', 'User'),
('40377', 'Paulus Budi Mulyono', 44, '', 6, '40355', '40751', '41053', '248e0936054f0ff1fa6135969174f162', 'User'),
('40386', 'Hadi Gunawan', 37, '', 6, '40349', '40751', '41053', 'eedec8cb8159470f2c25a22d1a9fef7c', 'User'),
('40410', 'I Ketut Suwetha', 21, '', 6, '40975', '41053', '', 'b28d9123c8c2bb408428a90d5598906f', 'User'),
('40418', 'Siat Tjie Djiang', 35, '', 6, '40751', '41053', '', '023e1de20a1a68aab1eeb0562b018ed4', 'User'),
('40419', 'Gede Jana Wiriawan', 58, '', 6, '41537', '41053', '', 'c61ab3254a8a0d55aa901aa8649460c9', 'User'),
('40421', 'Arie Christian', 50, '', 6, '40975', '41053', '', 'b88d1a4d8194f43a664ab37ba3d65461', 'User'),
('40529', 'Hadjar Dwi Tjahjono', 17, '', 4, '40786', '', '', '5625c3aac56c198ad77688028e08a4c3', 'User'),
('40605', 'Erick Chandra Lionardi', 22, '', 5, '41153', '', '', 'dd61b41d5b91a9a09e504f025a87553b', 'User'),
('40699', 'Ari Wibawanti', 10, '', 2, '40880', '', '41521', 'dbf3005b46d0a2f32b8fd89ba4043946', 'User'),
('40709', 'Meta Dwi Yulfita', 16, '', 4, '40786', '', '', 'ea344d40ba4d83dcb5e08328b1789614', 'User'),
('40726', 'Fifi Amaliawati Nursalim', 53, '', 6, '41053', '', '', '7209375983416cef4eb2d1007716ce7e', 'User'),
('40730', 'Johnny Ohisto', 21, '', 6, '40975', '41053', '', '60803ea798a0c0dfb7f36397d8d4d772', 'User'),
('40731', 'Herman Subrata', 51, '', 6, '41053', '', '', 'ed0fa43d037c5d43cf136a6f17671aae', 'User'),
('40751', 'Rianto Dwi Leksono', 31, '', 6, '41053', '', '', 'ce3c9c905135bab43c25500d3435a7a7', 'User'),
('40786', 'Wuu Sok Lan', 1, '', 4, '', '', '', 'cdca3a8105fb4877c4e0f5a37c5dff23', 'User'),
('40788', 'Ni Nyoman Supraptiningsih', 49, '', 6, '40975', '41053', '', 'ce4b094fc70468917d37378c7f0175d1', 'User'),
('40815', 'Gideon Budiyanto Supangat', 21, '', 5, '40605', '41153', '', '50901a6542ade98ecedba98b403134bf', 'User'),
('40819', 'Yetty Salome Tampubolon', 5, '', 1, '40889', '', '', '83736f851256ebfd392d677baefc775d', 'User'),
('40833', 'Muhammad Guntur Susetiyono', 57, '', 6, '41537', '41053', '', '82aa105255909a3fc232c2a53821ebd2', 'User'),
('40837', 'Fiyarni Moeslim Pamuntjak', 6, '', 1, '40819', '40889', '', 'e3b1fdbfb97eddd5f1dcc781498f1b47', 'User'),
('40838', 'Narwin', 9, '', 2, '40880', '41521', '', 'b937a56ac2082006f7b0a40ab60fe958', 'User'),
('40880', 'Sutrisno', 8, '', 2, '41521', '', '', '2af3050c976c65da41266c29f8274cbd', 'User'),
('40889', 'Linda Anthonius', 1, '', 1, '', '', '', '7c04ffdf63fedddcb42474caf8c06540', 'User'),
('40898', 'Aries Dwi Nugroho', 10, '', 2, '40880', '41521', '', '1b7b2f2e63a4aee4d9a766abdd911efb', 'User'),
('40918', 'Rheinhard Artasasta Napitupulu', 19, '', 4, '40786', '', '', '01409f7f7f2bd5ec0c1755b362e9db6e', 'User'),
('40932', 'Adid Aviv Himmawan', 13, '', 3, '41093', '', '', '9a9ba3a587dbfe50928c9a6e294244e8', 'User'),
('40948', 'Tumpak Benny Marpaung', 3, '', 1, '40889', '', '', 'f8f2394347595ff31db6d61e584dc346', 'User'),
('40975', 'Robertus Cangkrama', 47, '', 6, '41053', '', '', '8e06eb57b289ac466aff1c3922d37439', 'User'),
('40989', 'Marlina Hartati', 4, '', 1, '40889', '', '', 'f5772c97a8e2bcf909674d8701480d55', 'User'),
('40997', 'Dwi Wibawa Tumpakusmara', 7, '', 1, '40889', '', '', '901c5103458da9452862157b5b4a2584', 'User'),
('41041', 'Rachmat Ansyari', 39, '', 6, '40349', '40751', '41053', '46fed9ea4097abfe7799de65486ee1dd', 'User'),
('41049', 'Rousye Silvia Marisi S', 2, '', 1, '40889', '', '', '4f9b784fbdde0c7a965fcbda042b2b9f', 'User'),
('41053', 'Tai Chau Peng', 1, '', 6, '', '', '', 'ce943be8824807573e3d363d01058edb', 'User'),
('41068', 'Hendrian', 42, '', 6, '40360', '40751', '41053', 'aedd45c2ac38b5cdbfac250d1031246a', 'User'),
('41092', 'Ananta Primusa', 4, '', 1, '40889', '', '', '9b27373ee26f5c75aa3ac8c862b21f08', 'User'),
('41093', 'Donny Dharma Gunawan', 11, '', 3, '', '', '', 'c9ae486d4193a28218ebcf092e1a1659', 'User'),
('41119', 'Sukarno', 56, '', 6, '41537', '41053', '', '12db1dbf2200bc9c5f1d04c69ae04ff0', 'User'),
('41151', 'Kurniawaty', 25, '', 3, '41093', '', '', 'fdd62f8bdc8bd18509c0d3b5159ffc8d', 'User'),
('41153', 'Mike Winzerling', 1, '', 5, '', '', '', '748aae853cec038ddf04d18e51be9438', 'User'),
('41156', 'Teo Beng Hock', 26, '', 5, '41153', '', '', 'b59362a85e1078197f5b82c0e96de5d8', 'User'),
('41158', 'Erwin Iwansyah', 21, '', 6, '40975', '41053', '', 'e3f37a80937016c28f4b687370f9783e', 'User'),
('41190', 'Didin Mujahidin', 28, '', 5, '41156', '41153', '', '9c6dc3ff593093879a0eaf172f13589a', 'User'),
('41234', 'Reynold Farid', 30, '', 5, '41156', '41153', '', '7cfc37ec1434434ea9e2c926984fd996', 'User'),
('41295', 'Herwandhoni', 10, '', 6, '40975', '41053', '', 'f3a589d86446fc3ecd5385a2779e8d34', 'User'),
('41319', 'Fithriyanti', 27, '', 5, '41156', '41153', '', '739914b0be9b5e872dd064ef8686172f', 'User'),
('41353', 'Prima Agung Prasetyo Wahyudi', 38, '', 6, '40349', '40751', '41053', '88c47a250dbd2212ac36d8513cd9ed80', 'User'),
('41374', 'Jans M Carlos Sitorus', 41, '', 6, '40360', '40751', '41053', '630b6159ab1a4be8b302739904f8beff', 'User'),
('41388', 'Ady Yudicium Mustika', 46, '', 6, '40355', '40751', '41053', '3bb76268c4e97fceb80480530d8ee462', 'User'),
('41396', 'Ami Wahju Siswanto', 34, '', 6, '40751', '41053', '', '9386d1ad527a46fb3189b7804747893b', 'User'),
('41424', 'Stephanus Goh', 23, '', 5, '40605', '41153', '', '3d25d97d74b25fda24861545538d0475', 'User'),
('41425', 'Yudhi Tri Alfiyono', 24, '', 5, '40605', '41153', '', '2008b6bfa59e4d81791f9ecbfd9f1a75', 'User'),
('41426', 'Sater Indrajati', 29, '', 5, '41156', '41153', '', '28c634381653ecb8ffcf146cd8caf34e', 'User'),
('41427', 'Demokrat Fajar Suharmanto', 14, '', 3, '41093', '', '', 'c6a3a2c5533022519407029f38d214fa', 'User'),
('41453', 'Sugiarto', 45, '', 6, '40355', '40751', '41053', '98e91749e0199da4b939761492530d23', 'User'),
('41461', 'Imelda Sari Puspita', 52, '', 6, '41053', '', '', '2f0033672b5d798d3367020fa452b7e3', 'User'),
('41470', 'Fatema Irianus Mendrofa', 15, '', 4, '40786', '', '', 'c22f68de5bdce96fee03e21ca08d898d', 'User'),
('41519', 'Hermawan Koesnadi', 25, '', 5, '40605', '41153', '', 'd13abd6e45a03b13d31e7d34d6f1c767', 'User'),
('41521', 'Lim Siew Tin', 1, '', 2, '', '', '', 'c8126ff04279310bed26167f226c67c0', 'User'),
('41533', 'Kiromul Abidin', 48, '', 6, '40975', '41053', '', 'e26042b30aef850d108a5014d1f3fe6c', 'User'),
('41537', 'Mancheri Ganapathy Viswanath', 55, '', 6, '41053', '', '', 'b334cac4ee40f5ccda7dc3d9f1e4f388', 'User');

-- --------------------------------------------------------

--
-- Table structure for table `facilities`
--

CREATE TABLE IF NOT EXISTS `facilities` (
  `id_facilities` int(11) NOT NULL AUTO_INCREMENT,
  `facilities` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_facilities`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=12 ;

--
-- Dumping data for table `facilities`
--

INSERT INTO `facilities` (`id_facilities`, `facilities`) VALUES
(1, 'Monthly Parking Sticker'),
(2, 'Parking Allowance'),
(3, 'IP Telephone'),
(4, 'EC 500'),
(5, 'Hand phone'),
(6, 'Hand phone Registration'),
(7, 'BlackBerry'),
(8, 'BlackBerry Registration'),
(9, 'Personal Computer - Desktop'),
(10, 'Personal Computer - Notebook'),
(11, 'Purchase New Asset');

-- --------------------------------------------------------

--
-- Table structure for table `highest_qualification`
--

CREATE TABLE IF NOT EXISTS `highest_qualification` (
  `id_highest_qualification` int(11) NOT NULL AUTO_INCREMENT,
  `highest_qualification` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_highest_qualification`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `highest_qualification`
--

INSERT INTO `highest_qualification` (`id_highest_qualification`, `highest_qualification`) VALUES
(1, 'High School'),
(2, 'Diploma Degree - D2'),
(3, 'Diploma Degree - D3'),
(4, 'Bachelor Degree (S1)'),
(5, 'Master/ Post Graduate Degree (S2)');

-- --------------------------------------------------------

--
-- Table structure for table `interview`
--

CREATE TABLE IF NOT EXISTS `interview` (
  `id_interview` int(11) NOT NULL AUTO_INCREMENT,
  `id_process` int(11) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `remarks` text,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id_interview`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `interview`
--

INSERT INTO `interview` (`id_interview`, `id_process`, `register_date`, `remarks`, `status`) VALUES
(1, 1, '2012-07-02', '<p>fhhhb</p>', NULL),
(2, 1, '2012-07-26', '<p>dgsgdgsd</p>', NULL),
(3, 2, '2012-07-10', '<p>dsgfd</p>', NULL),
(4, 2, '2012-07-05', '<p>hjfjdf</p>', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `join_employee`
--

CREATE TABLE IF NOT EXISTS `join_employee` (
  `id_join_employee` int(11) NOT NULL AUTO_INCREMENT,
  `id_process` int(11) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `remarks` text,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id_join_employee`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `join_employee`
--

INSERT INTO `join_employee` (`id_join_employee`, `id_process`, `register_date`, `remarks`, `status`) VALUES
(1, 1, '2012-07-21', '<p>lkkjljk</p>', NULL),
(2, 1, '2012-07-24', '<p>gfdgd</p>', NULL),
(3, 2, '2012-07-17', '<p>zxvcxbnvfr</p>', NULL),
(4, 2, '2012-07-27', '<p>uyiouyotryuhf</p>', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `mcu_process`
--

CREATE TABLE IF NOT EXISTS `mcu_process` (
  `id_mcu_process` int(11) NOT NULL AUTO_INCREMENT,
  `id_process` int(11) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `remarks` text,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id_mcu_process`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `mcu_process`
--

INSERT INTO `mcu_process` (`id_mcu_process`, `id_process`, `register_date`, `remarks`, `status`) VALUES
(1, 1, '2012-07-10', '<p>jgjsd</p>', NULL),
(2, 1, '2012-07-20', '<p>aSFHJ</p>', NULL),
(3, 2, '2012-07-17', '<p>JDGFD</p>', NULL),
(4, 2, '2012-07-29', '<p>gfsghjg</p>', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `offering_process`
--

CREATE TABLE IF NOT EXISTS `offering_process` (
  `id_offering_process` int(11) NOT NULL AUTO_INCREMENT,
  `id_process` int(11) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `remarks` text,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id_offering_process`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `offering_process`
--

INSERT INTO `offering_process` (`id_offering_process`, `id_process`, `register_date`, `remarks`, `status`) VALUES
(1, 1, '2012-07-10', '<p>tryure</p>', NULL),
(2, 1, '2012-07-20', '<p>trtyu54f</p>', NULL),
(3, 2, '2012-07-16', '<p>sghdasghsa</p>', NULL),
(4, 2, '2012-07-23', '<p>dgsghsd</p>', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `position`
--

CREATE TABLE IF NOT EXISTS `position` (
  `id_position` int(11) NOT NULL AUTO_INCREMENT,
  `position` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_position`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=59 ;

--
-- Dumping data for table `position`
--

INSERT INTO `position` (`id_position`, `position`) VALUES
(1, 'President Director'),
(2, 'Credit Controller'),
(3, 'Commercial Manager'),
(4, 'Finance Manager'),
(5, 'Human Resource Manager'),
(6, 'Recruitment & Training Manager'),
(7, 'Senior IT Support Executive'),
(8, 'Div Manager Specialty Chem'),
(9, 'Sales Manager for Plastic & Rubber'),
(10, 'Assistant Sales Manager'),
(11, '(Acting) General Manager'),
(12, 'Manager, Business Support'),
(13, 'Manager, Service Operation'),
(14, 'Business Development Manager'),
(15, 'Divisional Manager - Turf & Irrigation'),
(16, 'Admin Executive'),
(17, 'Technical Manager'),
(18, 'Logistic Manager'),
(19, 'Service Manager'),
(20, 'Division Sales Manager'),
(21, 'Sales Manager'),
(22, 'General Manager Domestic Sales'),
(23, 'Sales Manager - Medan'),
(24, 'Sales Manager - Surabaya'),
(25, 'Business Support Manager'),
(26, 'General Manager Technical & Production'),
(27, 'Quality Assurance Manager'),
(28, 'Plant Manager'),
(29, 'Logistics Manager'),
(30, 'Product Development and Costing Manager'),
(31, 'General Manager - Service'),
(32, 'Business Admin Manager - Service'),
(33, 'Service Support & Training Manager'),
(34, 'Parts Development Manager'),
(35, 'Refurbishment Manager'),
(36, 'Assistant Divisional Manager'),
(37, 'Service Manager - Surabaya'),
(38, 'Service Manager Minning'),
(39, 'Senior Service Engineer'),
(40, 'Area Service Manager'),
(41, 'Service Manager - Pekanbaru'),
(42, 'Senior Sales Engineer'),
(44, 'Service Manager - Jakarta'),
(45, 'Service Contract Manager'),
(46, 'Service Manager - CCE'),
(47, 'Division Manager - Equipment'),
(48, 'Assistant Sales Manager - PKU'),
(49, 'Sales Administrator'),
(50, 'Product Manager'),
(51, 'Assistant Manager, Reg IT Infrastructur'),
(52, 'Administration Manager'),
(53, 'Senior Marketing Executive'),
(54, 'Quality and Facility Manager'),
(55, 'General Manager - Fulfillment'),
(56, 'Material Manager'),
(57, 'Production Manager'),
(58, 'Project Manager');

-- --------------------------------------------------------

--
-- Table structure for table `position_level`
--

CREATE TABLE IF NOT EXISTS `position_level` (
  `id_position_level` int(11) NOT NULL AUTO_INCREMENT,
  `position_level` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_position_level`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Dumping data for table `position_level`
--

INSERT INTO `position_level` (`id_position_level`, `position_level`) VALUES
(1, 'Manager/ Assistant Manager'),
(2, 'Senior Executive – Senior Staff '),
(3, 'Supervisor/ Coordinator/ Team Leader'),
(4, 'Junior Staff (non-management & non-supervisor)'),
(5, 'Fresh Graduate/ Less than 1 year experience'),
(6, 'Skilled worker (Operator, Technician)');

-- --------------------------------------------------------

--
-- Table structure for table `process`
--

CREATE TABLE IF NOT EXISTS `process` (
  `id_process` int(11) NOT NULL AUTO_INCREMENT,
  `id_request` int(11) DEFAULT NULL,
  `date_in` date DEFAULT NULL,
  `date_finish` date DEFAULT NULL,
  `status` int(11) DEFAULT NULL,
  `remarks` text NOT NULL,
  PRIMARY KEY (`id_process`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `process`
--

INSERT INTO `process` (`id_process`, `id_request`, `date_in`, `date_finish`, `status`, `remarks`) VALUES
(1, 1, '2012-07-29', '0000-00-00', 1, ''),
(2, 1, '2012-07-29', '0000-00-00', 1, ''),
(3, 2, '2012-07-29', '0000-00-00', 0, ''),
(4, 2, '2012-07-29', '0000-00-00', 0, ''),
(5, 2, '2012-07-29', '0000-00-00', 0, '');

-- --------------------------------------------------------

--
-- Table structure for table `register_resources`
--

CREATE TABLE IF NOT EXISTS `register_resources` (
  `id_register_resources` int(11) NOT NULL AUTO_INCREMENT,
  `id_process` int(11) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `remarks` text,
  `status` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id_register_resources`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `register_resources`
--

INSERT INTO `register_resources` (`id_register_resources`, `id_process`, `register_date`, `remarks`, `status`) VALUES
(1, 1, '2012-07-02', '<p>asdf</p>', NULL),
(2, 1, '2012-07-17', '<p>halo</p>', NULL),
(4, 2, '2012-07-11', '<p>hmmm</p>', NULL),
(5, 2, '2012-07-01', '<p>asikkk</p>', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `request`
--

CREATE TABLE IF NOT EXISTS `request` (
  `id_request` int(11) NOT NULL AUTO_INCREMENT,
  `id_employee` char(7) DEFAULT NULL,
  `request_date` date DEFAULT NULL,
  `id_position` int(11) DEFAULT NULL,
  `salary` int(11) DEFAULT NULL,
  `date_needed` date DEFAULT NULL,
  `job_grade` varchar(50) DEFAULT NULL,
  `hay_points` varchar(100) DEFAULT NULL,
  `status_vacancies` enum('Permanent','Temporary') DEFAULT NULL,
  `contract_date_from` date DEFAULT NULL,
  `contract_date_to` date DEFAULT NULL,
  `job_description` text,
  `major_responsibilities` text,
  `authority` text,
  `number_of_direct_report` varchar(255) DEFAULT NULL,
  `id_highest_qualification` int(11) DEFAULT NULL,
  `years_of_experience` enum('Less than 2 years','3 to 5 years','6 – 10 years','More than  10 years') NOT NULL,
  `candidate_current_company` varchar(255) DEFAULT NULL,
  `personal_traits` text,
  `total_need` int(11) NOT NULL,
  `approver_1` char(7) DEFAULT NULL,
  `approver_2` char(7) DEFAULT NULL,
  `approver_3` char(7) DEFAULT NULL,
  `status_recruitment` enum('New Employee','Replacement') DEFAULT NULL,
  `id_employee_replacement` char(7) NOT NULL,
  `date_terminated` date NOT NULL,
  `status_request` int(1) NOT NULL,
  PRIMARY KEY (`id_request`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `request`
--

INSERT INTO `request` (`id_request`, `id_employee`, `request_date`, `id_position`, `salary`, `date_needed`, `job_grade`, `hay_points`, `status_vacancies`, `contract_date_from`, `contract_date_to`, `job_description`, `major_responsibilities`, `authority`, `number_of_direct_report`, `id_highest_qualification`, `years_of_experience`, `candidate_current_company`, `personal_traits`, `total_need`, `approver_1`, `approver_2`, `approver_3`, `status_recruitment`, `id_employee_replacement`, `date_terminated`, `status_request`) VALUES
(1, '40066', '2012-07-29', 16, 5000000, '2012-09-01', 'DD', 'DD', 'Permanent', NULL, NULL, NULL, NULL, NULL, NULL, 4, 'Less than 2 years', 'Test', NULL, 2, '40786', '', '', 'New Employee', '', '0000-00-00', 4),
(2, '40837', '2012-07-29', NULL, 3000000, '2012-07-02', 'A', 'd', 'Temporary', '2012-07-16', '2013-07-22', 'sfdgsdf', 'xgjdfghdf', 'dhgfhdfghf', '2', 2, 'Less than 2 years', 'aaa', 'dghfg', 3, '40819', '40889', '', 'New Employee', '', '0000-00-00', 0);

-- --------------------------------------------------------

--
-- Table structure for table `request_current_industry`
--

CREATE TABLE IF NOT EXISTS `request_current_industry` (
  `id_request` int(11) DEFAULT NULL,
  `id_current_industry` int(11) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `request_current_industry`
--

INSERT INTO `request_current_industry` (`id_request`, `id_current_industry`, `remarks`) VALUES
(2, 5, NULL),
(3, 2, NULL),
(1, 1, NULL),
(2, 4, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `request_facilities`
--

CREATE TABLE IF NOT EXISTS `request_facilities` (
  `id_request` int(11) DEFAULT NULL,
  `id_facilities` int(11) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `request_position_level`
--

CREATE TABLE IF NOT EXISTS `request_position_level` (
  `id_request` int(11) DEFAULT NULL,
  `id_position_level` int(11) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `request_position_level`
--

INSERT INTO `request_position_level` (`id_request`, `id_position_level`, `remarks`) VALUES
(2, 2, NULL),
(3, 2, NULL),
(1, 5, NULL),
(2, 6, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `request_soft_skills`
--

CREATE TABLE IF NOT EXISTS `request_soft_skills` (
  `id_request` int(11) DEFAULT NULL,
  `id_soft_skills` int(11) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `request_soft_skills`
--

INSERT INTO `request_soft_skills` (`id_request`, `id_soft_skills`, `remarks`) VALUES
(2, 9, NULL),
(3, 10, NULL),
(1, 14, NULL),
(2, 19, NULL),
(2, 4, NULL),
(2, 15, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `request_specialized_skills`
--

CREATE TABLE IF NOT EXISTS `request_specialized_skills` (
  `id_request` int(11) DEFAULT NULL,
  `id_specialized_skills` int(11) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `request_specialized_skills`
--

INSERT INTO `request_specialized_skills` (`id_request`, `id_specialized_skills`, `remarks`) VALUES
(2, 54, NULL),
(3, 7, NULL),
(1, 8, NULL),
(2, 4, NULL),
(2, 43, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `request_study`
--

CREATE TABLE IF NOT EXISTS `request_study` (
  `id_request` int(11) DEFAULT NULL,
  `id_study` int(11) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `request_study`
--

INSERT INTO `request_study` (`id_request`, `id_study`, `remarks`) VALUES
(3, 17, NULL),
(1, 18, NULL),
(2, 19, NULL),
(2, 23, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `sending_candidates_cv`
--

CREATE TABLE IF NOT EXISTS `sending_candidates_cv` (
  `id_sending_candidates_cv` int(11) NOT NULL AUTO_INCREMENT,
  `id_process` int(11) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `remarks` text,
  `file` varchar(255) NOT NULL,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id_sending_candidates_cv`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `sending_candidates_cv`
--

INSERT INTO `sending_candidates_cv` (`id_sending_candidates_cv`, `id_process`, `register_date`, `remarks`, `file`, `status`) VALUES
(1, 1, '2012-07-11', '<p>siang</p>', 'c4a55-Staff-Req-2012.doc', NULL),
(2, 1, '2012-07-26', '<p>malam</p>', 'cd0fd-Staff-Req-2012.doc', NULL),
(4, 2, '2012-07-12', '<p>kjapan</p>', '88f22-Staff-Req-2012.doc', NULL),
(5, 2, '2012-07-11', '<p>fdsfd</p>', 'aae17-Staff-Req-2012.doc', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `shl_test`
--

CREATE TABLE IF NOT EXISTS `shl_test` (
  `id_shl_test` int(11) NOT NULL AUTO_INCREMENT,
  `id_process` int(11) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `remarks` text,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id_shl_test`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `shl_test`
--

INSERT INTO `shl_test` (`id_shl_test`, `id_process`, `register_date`, `remarks`, `status`) VALUES
(1, 1, '2012-07-10', '<p>gjgjfjf</p>', NULL),
(2, 1, '2012-07-23', '<p>fgadsfgb</p>', NULL),
(3, 2, '2012-07-03', '<p>kgjhfsg</p>', NULL),
(4, 2, '2012-07-17', '<p>FDSGHG</p>', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `soft_skills`
--

CREATE TABLE IF NOT EXISTS `soft_skills` (
  `id_soft_skills` int(11) NOT NULL AUTO_INCREMENT,
  `soft_skills` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_soft_skills`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=21 ;

--
-- Dumping data for table `soft_skills`
--

INSERT INTO `soft_skills` (`id_soft_skills`, `soft_skills`) VALUES
(1, 'Deciding and Initiating Action'),
(2, 'Leading and Supervising'),
(3, 'Working with People'),
(4, 'Adhering to Principles & Values'),
(5, 'Learning and Researching => R&D, Researcher'),
(6, 'Formulating Strategies & Concepts'),
(7, 'Creating & Innovating'),
(8, 'Writing and Reporting => Journalist, Reporter'),
(9, 'Applying Expertise and Technology'),
(10, 'Analysing'),
(11, 'Relating and Networking'),
(12, 'Persuading and Influencing'),
(13, 'Presenting and Communicating Information'),
(14, 'Achieving Personal Work Goals & Objectives'),
(15, 'Entrepreneurial and Commercial Thinking'),
(16, 'Planning and Organising'),
(17, 'Delivering Results & Meeting Customer Expectations'),
(18, 'Following Instructions and Procedure'),
(19, 'Adapting and Responding to Change'),
(20, 'Coping with Pressures and Setbacks');

-- --------------------------------------------------------

--
-- Table structure for table `specialized_skills`
--

CREATE TABLE IF NOT EXISTS `specialized_skills` (
  `id_specialized_skills` int(11) NOT NULL AUTO_INCREMENT,
  `specialized_skills` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_specialized_skills`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=70 ;

--
-- Dumping data for table `specialized_skills`
--

INSERT INTO `specialized_skills` (`id_specialized_skills`, `specialized_skills`) VALUES
(1, 'Agriculture'),
(2, 'Forestry'),
(3, 'Fisheries'),
(4, 'Architecture'),
(5, 'Interior Design'),
(6, 'Chemistry'),
(7, 'Clerical'),
(8, 'Administrative Support'),
(9, 'Customer Service'),
(10, 'Front liners'),
(11, 'Engineering – Chemicals'),
(12, 'Engineering – Civil'),
(13, 'Engineering – Construction'),
(14, 'Engineering – Structural'),
(15, 'Engineering – Electrical'),
(16, 'Engineering – Communication'),
(17, 'Engineering – IT'),
(18, 'Engineering – Industrial'),
(19, 'Engineering – Mechanicals'),
(20, 'Engineering – Oil & Gas'),
(21, 'Finance – Audit'),
(22, 'Finance – Taxation'),
(23, 'Finance – Corporate'),
(24, 'Finance – Cost Accounting'),
(25, 'Finance – General'),
(26, 'General Work - Housekeeper'),
(27, 'General Work - Driver'),
(28, 'General Work - Dispatcher'),
(29, 'General Work - Messenger'),
(30, 'Human Resources – Compensation & Benefit'),
(31, 'Human Resources – General Admin'),
(32, 'Human Resources – Industrial Relations'),
(33, 'Human Resources – Recruitment'),
(34, 'Human Resources – Training & Development'),
(35, 'IT – Hardware'),
(36, 'IT – Network'),
(37, 'IT – System'),
(38, 'IT – Database Admin'),
(39, 'IT – Software'),
(40, 'Logistic'),
(41, 'Supply Chain'),
(42, 'Marketing'),
(43, 'Business Development'),
(44, 'Office Management'),
(45, 'Operator – Manufacturing'),
(46, 'Operator – Production'),
(47, 'Process Design & Control'),
(48, 'Product Design & Costing'),
(49, 'Project Management'),
(50, 'Purchasing'),
(51, 'Inventory'),
(52, 'Material & Warehouse Management'),
(53, 'Quality Control'),
(54, 'Assurance'),
(55, 'Quantity Surveying'),
(56, 'Sales –Corporate'),
(57, 'Sales – Engineering'),
(58, 'Sales – Technical'),
(59, 'Sales – Retail'),
(60, 'Sales – General'),
(61, 'Sales – Telesales'),
(62, 'Sales – Telemarketing'),
(63, 'Sales – Science & Technology'),
(64, 'Secretarial'),
(65, 'Executive & Personal Assistant'),
(66, 'Technical – Helpdesk Support'),
(67, 'Technician – Installation'),
(68, 'Technician – Repair & Maintenance'),
(69, 'Others');

-- --------------------------------------------------------

--
-- Table structure for table `standart_test`
--

CREATE TABLE IF NOT EXISTS `standart_test` (
  `id_standart_test` int(11) NOT NULL AUTO_INCREMENT,
  `id_process` int(11) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `remarks` text,
  `status` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id_standart_test`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Dumping data for table `standart_test`
--

INSERT INTO `standart_test` (`id_standart_test`, `id_process`, `register_date`, `remarks`, `status`) VALUES
(1, 1, '2012-07-10', '<p>oho</p>', NULL),
(2, 1, '2012-07-16', '<p>ehee</p>', NULL),
(3, 1, '2012-07-10', '<p>sadfsa</p>', NULL),
(4, 1, '2012-07-26', '<p>fghqet</p>', NULL),
(5, 2, '2012-07-16', '<p>gdsg</p>', NULL),
(6, 2, '2012-07-19', '<p>asdwee</p>', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `study`
--

CREATE TABLE IF NOT EXISTS `study` (
  `id_study` int(11) NOT NULL AUTO_INCREMENT,
  `study` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_study`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=38 ;

--
-- Dumping data for table `study`
--

INSERT INTO `study` (`id_study`, `study`) VALUES
(1, 'Engineering – Chemicals'),
(2, 'Engineering – Civil'),
(3, 'Engineering – Construction'),
(4, 'Engineering – Civil Structural'),
(5, 'Engineering – Computer'),
(6, 'Engineering – Telecommunication'),
(7, 'Engineering – Electrical'),
(8, 'Engineering – Electronic'),
(9, 'Engineering – Industrial'),
(10, 'Engineering – Mechanicals'),
(11, 'Engineering – Mechatronic'),
(12, 'Engineering – Electromechanical'),
(13, 'Engineering – Mining'),
(14, 'Engineering – Mineral'),
(15, 'Engineering – Petroleum'),
(16, 'Engineering – Oil'),
(17, 'Engineering – Gas'),
(18, 'Agriculture'),
(19, 'Aquaculture'),
(20, 'Forestry'),
(21, 'Biology'),
(22, 'Bio Technology'),
(23, 'Chemistry'),
(24, 'Food Technology'),
(25, 'Nutrition'),
(26, 'Business Administration'),
(27, 'Management'),
(28, 'Economics'),
(29, 'Finance/ Accounting'),
(30, 'Human Resources Management'),
(31, 'Marketing'),
(32, 'Secretarial'),
(33, 'Commerce'),
(34, 'Medical Science & Technology'),
(35, 'Pharmacy'),
(36, 'Pharmacology'),
(37, 'Veterinary');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
