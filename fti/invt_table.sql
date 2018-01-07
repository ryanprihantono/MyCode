-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Sep 15, 2012 at 04:12 AM
-- Server version: 5.5.16
-- PHP Version: 5.3.8

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
-- Table structure for table `tr_pengalihan`
--

CREATE TABLE IF NOT EXISTS `tr_pengalihan` (
  `id_barang` int(11) NOT NULL,
  `id_pengalihan` int(11) NOT NULL,
  `remark` varchar(255) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id_barang`,`id_pengalihan`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tr_pengalihan`
--

INSERT INTO `tr_pengalihan` (`id_barang`, `id_pengalihan`, `remark`) VALUES
(2147483647, 20, '0');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
