-- phpMyAdmin SQL Dump
-- version 3.5.2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Oct 02, 2012 at 03:50 PM
-- Server version: 5.5.25a
-- PHP Version: 5.4.4

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `invt`
--

-- --------------------------------------------------------

--
-- Table structure for table `tr_page`
--

CREATE TABLE IF NOT EXISTS `tr_page` (
  `id_page` int(11) NOT NULL,
  `id_role` int(11) NOT NULL,
  `remark` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tr_page`
--

INSERT INTO `tr_page` (`id_page`, `id_role`, `remark`) VALUES
(2, 1, '0'),
(6, 1, '1'),
(1, 1, '2'),
(8, 1, '3'),
(7, 1, '4'),
(11, 1, '5'),
(3, 1, '6'),
(4, 1, '7'),
(5, 1, '8'),
(10, 1, '9'),
(9, 1, '10');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
