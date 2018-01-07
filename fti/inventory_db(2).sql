-- phpMyAdmin SQL Dump
-- version 3.4.5
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Sep 15, 2012 at 02:14 AM
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
-- Table structure for table `barang`
--

CREATE TABLE IF NOT EXISTS `barang` (
  `id_barang` int(11) NOT NULL,
  `nama_barang` varchar(20) NOT NULL,
  `id_kategori` int(11) NOT NULL,
  `merk` varchar(255) NOT NULL,
  `spesifikasi` varchar(255) NOT NULL,
  `kondisi` varchar(10) NOT NULL,
  `id_ruang` int(11) NOT NULL,
  `tanggal_pembelian` date NOT NULL,
  `harga` int(11) NOT NULL,
  PRIMARY KEY (`id_barang`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1;

--
-- Dumping data for table `barang`
--

INSERT INTO `barang` (`id_barang`, `nama_barang`, `id_kategori`, `merk`, `spesifikasi`, `kondisi`, `id_ruang`, `tanggal_pembelian`, `harga`) VALUES
(2147483647, 'kamera', 1, 'Canon', 'E5000', 'baik', 1, '2012-08-14', 10000),
(2147483643, 'komputer', 1, 'asd', 'asdf', 'baik', 1, '2012-08-23', 4400000),
(2147483641, 'meja', 2, 'sdfad', 'dsfdsf', 'baik', 25, '2012-08-21', 6000000);

-- --------------------------------------------------------

--
-- Table structure for table `history`
--
-- in use(#1356 - View 'invt.history' references invalid table(s) or column(s) or function(s) or definer/invoker of view lack rights to use them)
-- in use (#1356 - View 'invt.history' references invalid table(s) or column(s) or function(s) or definer/invoker of view lack rights to use them)

-- --------------------------------------------------------

--
-- Table structure for table `kategori_brg`
--

CREATE TABLE IF NOT EXISTS `kategori_brg` (
  `id_kategori` int(11) NOT NULL AUTO_INCREMENT,
  `kategori_brg` varchar(15) NOT NULL,
  PRIMARY KEY (`id_kategori`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `kategori_brg`
--

INSERT INTO `kategori_brg` (`id_kategori`, `kategori_brg`) VALUES
(1, 'Elektronik'),
(2, 'Meubel');

-- --------------------------------------------------------

--
-- Table structure for table `kategori_ruang`
--

CREATE TABLE IF NOT EXISTS `kategori_ruang` (
  `id_kategori_ruang` int(11) NOT NULL AUTO_INCREMENT,
  `kategori_ruang` varchar(10) NOT NULL,
  PRIMARY KEY (`id_kategori_ruang`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `kategori_ruang`
--

INSERT INTO `kategori_ruang` (`id_kategori_ruang`, `kategori_ruang`) VALUES
(1, 'Kelas'),
(2, 'Auditorium');

-- --------------------------------------------------------

--
-- Table structure for table `pembelian`
--

CREATE TABLE IF NOT EXISTS `pembelian` (
  `id_pembelian` int(11) NOT NULL AUTO_INCREMENT,
  `id_barang` varchar(50) NOT NULL,
  `merk` varchar(255) NOT NULL,
  `spesifikasi` varchar(255) NOT NULL,
  `tanggal` date NOT NULL,
  `harga` int(11) NOT NULL,
  PRIMARY KEY (`id_pembelian`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `pembelian`
--

INSERT INTO `pembelian` (`id_pembelian`, `id_barang`, `merk`, `spesifikasi`, `tanggal`, `harga`) VALUES
(2, '24624351345', 'Sony', 'E50000', '2012-08-08', 100000),
(3, '65464564562', 'Toshiba', 'A', '2012-08-14', 30000);

-- --------------------------------------------------------

--
-- Table structure for table `pengalihan`
--

CREATE TABLE IF NOT EXISTS `pengalihan` (
  `id_pengalihan` int(11) NOT NULL AUTO_INCREMENT,
  `lokasi_asal` int(11) NOT NULL,
  `lokasi_tujuan` int(11) NOT NULL,
  `tanggal` date NOT NULL,
  PRIMARY KEY (`id_pengalihan`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=21 ;

--
-- Dumping data for table `pengalihan`
--

INSERT INTO `pengalihan` (`id_pengalihan`, `lokasi_asal`, `lokasi_tujuan`, `tanggal`) VALUES
(20, 1, 25, '2012-09-06');

-- --------------------------------------------------------

--
-- Table structure for table `perbaikan`
--

CREATE TABLE IF NOT EXISTS `perbaikan` (
  `id_perbaikan` int(11) NOT NULL AUTO_INCREMENT,
  `keterangan_kerusakan` varchar(255) NOT NULL,
  `tanggal` date NOT NULL,
  PRIMARY KEY (`id_perbaikan`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `perbaikan`
--

INSERT INTO `perbaikan` (`id_perbaikan`, `keterangan_kerusakan`, `tanggal`) VALUES
(1, 'cdsaca', '2012-08-20'),
(2, 'ers', '2012-08-14');

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE IF NOT EXISTS `roles` (
  `id_role` int(11) NOT NULL AUTO_INCREMENT,
  `role` varchar(255) COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`id_role`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=5 ;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`id_role`, `role`) VALUES
(1, 'admin'),
(2, 'Kepala Sarana Prasarana'),
(3, 'Laboran Staff'),
(4, 'Laboran Mahasiswa');

-- --------------------------------------------------------

--
-- Table structure for table `ruangan`
--

CREATE TABLE IF NOT EXISTS `ruangan` (
  `id_ruang` int(11) NOT NULL AUTO_INCREMENT,
  `nama_ruang` varchar(15) NOT NULL,
  `id_kategori_ruang` int(11) NOT NULL,
  `gedung` varchar(10) NOT NULL,
  PRIMARY KEY (`id_ruang`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=26 ;

--
-- Dumping data for table `ruangan`
--

INSERT INTO `ruangan` (`id_ruang`, `nama_ruang`, `id_kategori_ruang`, `gedung`) VALUES
(1, 'R32', 1, 'F'),
(25, '611', 1, 'M');

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

-- --------------------------------------------------------

--
-- Table structure for table `tr_perbaikan`
--

CREATE TABLE IF NOT EXISTS `tr_perbaikan` (
  `id_barang` int(11) NOT NULL,
  `id_perbaikan` int(11) NOT NULL,
  `remark` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tr_perbaikan`
--

INSERT INTO `tr_perbaikan` (`id_barang`, `id_perbaikan`, `remark`) VALUES
(2147483647, 1, ''),
(2147483647, 1, ''),
(2147483647, 2, ''),
(2147483647, 2, '');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_role` int(11) NOT NULL DEFAULT '1',
  `username` varchar(25) COLLATE utf8_bin NOT NULL,
  `password` varchar(34) COLLATE utf8_bin NOT NULL,
  `email` varchar(100) COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=25 ;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `id_role`, `username`, `password`, `email`) VALUES
(5, 1, 'admin', '21232f297a57a5a743894a0e4a801fc3', 'admin@localhost.com'),
(22, 3, 'rp', '00639c71ba1dbde84db84b3eb15d6820', 'rp'),
(23, 4, 'orang', '3bdccbfebc8d26235e58787c1e9e9737', 'orang@qq.com'),
(24, 2, 'ganteng', '8b6bc5d8046c8466359d3ac43ce362ab', 'ganteng@yahoo.com');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
