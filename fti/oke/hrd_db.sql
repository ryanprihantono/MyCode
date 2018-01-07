-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Jul 28, 2012 at 08:12 AM
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
-- Table structure for table `company`
--

CREATE TABLE IF NOT EXISTS `company` (
  `id_company` int(11) NOT NULL AUTO_INCREMENT,
  `company` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_company`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

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
  PRIMARY KEY (`id_employee`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `facilities`
--

CREATE TABLE IF NOT EXISTS `facilities` (
  `id_facilities` int(11) NOT NULL AUTO_INCREMENT,
  `facilities` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_facilities`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `highest_qualification`
--

CREATE TABLE IF NOT EXISTS `highest_qualification` (
  `id_highest_qualification` int(11) NOT NULL AUTO_INCREMENT,
  `highest_qualification` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_highest_qualification`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

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
(20, 'Division Sales Manger'),
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
  `status_vacancies` int(11) DEFAULT NULL,
  `contract_date_from` date DEFAULT NULL,
  `contract_date_to` date DEFAULT NULL,
  `job_description` varchar(255) DEFAULT NULL,
  `major_responsibilities` varchar(255) DEFAULT NULL,
  `authority` varchar(255) DEFAULT NULL,
  `number_or_direct_report` varchar(255) DEFAULT NULL,
  `id_highest_qualification` int(11) DEFAULT NULL,
  `candidate_current_company` varchar(255) DEFAULT NULL,
  `personal_traits` varchar(255) DEFAULT NULL,
  `id_employee_replacement` int(11) DEFAULT NULL,
  `date_terminated` date DEFAULT NULL,
  `approver_1` tinyint(1) DEFAULT NULL,
  `approver_2` tinyint(1) DEFAULT NULL,
  `approver_3` tinyint(1) DEFAULT NULL,
  `status_recruitment` enum('New Employee','Replacement') DEFAULT NULL,
  PRIMARY KEY (`id_request`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `request_current_industry`
--

CREATE TABLE IF NOT EXISTS `request_current_industry` (
  `id_request` int(11) DEFAULT NULL,
  `id_current_industry` int(11) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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

-- --------------------------------------------------------

--
-- Table structure for table `request_soft_skills`
--

CREATE TABLE IF NOT EXISTS `request_soft_skills` (
  `id_request` int(11) DEFAULT NULL,
  `id_soft_skills` int(11) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `request_specialized_skills`
--

CREATE TABLE IF NOT EXISTS `request_specialized_skills` (
  `id_request` int(11) DEFAULT NULL,
  `id_specialized_skills` int(11) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `request_study`
--

CREATE TABLE IF NOT EXISTS `request_study` (
  `id_request` int(11) DEFAULT NULL,
  `id_study` int(11) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `soft_skills`
--

CREATE TABLE IF NOT EXISTS `soft_skills` (
  `id_soft_skills` int(11) NOT NULL AUTO_INCREMENT,
  `soft_skills` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_soft_skills`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `specialized_skills`
--

CREATE TABLE IF NOT EXISTS `specialized_skills` (
  `id_specialized_skills` int(11) NOT NULL AUTO_INCREMENT,
  `specialized_skills` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_specialized_skills`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `study`
--

CREATE TABLE IF NOT EXISTS `study` (
  `id_study` int(11) NOT NULL AUTO_INCREMENT,
  `study` varchar(255) DEFAULT NULL,
  `status` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_study`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `username` varchar(255) NOT NULL,
  `password` varchar(100) NOT NULL,
  `id_employee` int(11) DEFAULT NULL,
  `status` int(1) NOT NULL,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`username`, `password`, `id_employee`, `status`) VALUES
('admin', '0192023a7bbd73250516f069df18b500', 0, 1);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
