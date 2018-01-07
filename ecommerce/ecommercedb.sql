-- phpMyAdmin SQL Dump
-- version 3.5.2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Nov 02, 2012 at 03:38 PM
-- Server version: 5.5.25a
-- PHP Version: 5.4.4

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `ecommercedb`
--

-- --------------------------------------------------------

--
-- Table structure for table `bank`
--

CREATE TABLE IF NOT EXISTS `bank` (
  `bank_id` int(11) NOT NULL AUTO_INCREMENT,
  `bank` varchar(255) DEFAULT NULL,
  `account_number` varchar(255) DEFAULT NULL,
  `account_name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`bank_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Dumping data for table `bank`
--

INSERT INTO `bank` (`bank_id`, `bank`, `account_number`, `account_name`) VALUES
(1, 'BCA', '5270123456', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `cart`
--

CREATE TABLE IF NOT EXISTS `cart` (
  `cart_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) DEFAULT NULL,
  `date` date DEFAULT NULL,
  `remark` varchar(255) DEFAULT NULL,
  `bill_code` varchar(255) DEFAULT NULL,
  `total_bill` int(11) DEFAULT NULL,
  PRIMARY KEY (`cart_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=15 ;

--
-- Dumping data for table `cart`
--

INSERT INTO `cart` (`cart_id`, `user_id`, `date`, `remark`, `bill_code`, `total_bill`) VALUES
(1, 2, '2012-10-04', 'remark', '488ee48477', 6000),
(2, 2, '2012-10-04', 'remark', '5e546f68b2', 18000),
(3, 2, '2012-10-04', 'remark', 'd1bb850c9e', 12000),
(4, 2, '2012-10-04', '', 'de38f21d83', 10000),
(5, 2, '2012-10-04', '', '7896ef5c17', 10000),
(6, 2, '2012-10-04', '', 'f7ad46abc2', 24000),
(7, 2, '2012-10-05', '', '12a585ec02', 6000),
(8, 2, '2012-10-05', '', 'd2bc08083d', 6000),
(9, 2, '2012-10-06', '', '33de92acdd', 6000),
(10, 2, '2012-10-06', '', 'e7214ad0df', 4000),
(11, 2, '2012-10-06', '', '0086a948e2', 4000),
(12, 2, '2012-10-15', '', 'b42d67041b', 4000),
(13, 2, '2012-10-17', '', '393e905d52', 8000),
(14, 2, '2012-11-02', '', 'ecd71a755b', 102000);

-- --------------------------------------------------------

--
-- Table structure for table `cart_detail`
--

CREATE TABLE IF NOT EXISTS `cart_detail` (
  `cart_id` int(11) NOT NULL,
  `item_id` int(11) NOT NULL,
  `size_id` int(11) NOT NULL,
  `color_id` int(11) NOT NULL,
  `qty` int(11) DEFAULT NULL,
  PRIMARY KEY (`cart_id`,`item_id`,`color_id`,`size_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cart_detail`
--

INSERT INTO `cart_detail` (`cart_id`, `item_id`, `size_id`, `color_id`, `qty`) VALUES
(1, 1, 3, 1, 1),
(1, 1, 3, 2, 2),
(2, 1, 3, 1, 4),
(2, 1, 3, 2, 5),
(3, 1, 3, 1, 2),
(3, 1, 3, 2, 4),
(4, 1, 3, 1, 3),
(4, 1, 3, 2, 2),
(5, 1, 3, 1, 2),
(5, 1, 3, 2, 3),
(6, 1, 3, 1, 7),
(6, 1, 3, 2, 5),
(7, 1, 3, 1, 1),
(7, 1, 3, 2, 2),
(8, 1, 3, 1, 1),
(8, 1, 3, 2, 2),
(9, 1, 3, 1, 2),
(9, 1, 3, 2, 1),
(10, 1, 3, 1, 1),
(10, 1, 3, 2, 1),
(11, 1, 3, 1, 1),
(11, 1, 3, 2, 1),
(12, 1, 3, 1, 1),
(12, 1, 3, 2, 1),
(13, 1, 0, 1, 2),
(13, 1, 0, 2, 2),
(14, 1, 0, 1, 26),
(14, 1, 0, 2, 25);

-- --------------------------------------------------------

--
-- Table structure for table `category`
--

CREATE TABLE IF NOT EXISTS `category` (
  `category_id` int(11) NOT NULL AUTO_INCREMENT,
  `category` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`category_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `category`
--

INSERT INTO `category` (`category_id`, `category`) VALUES
(1, 'Local'),
(2, 'Import');

-- --------------------------------------------------------

--
-- Table structure for table `color`
--

CREATE TABLE IF NOT EXISTS `color` (
  `color_id` int(11) NOT NULL AUTO_INCREMENT,
  `color` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`color_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `color`
--

INSERT INTO `color` (`color_id`, `color`) VALUES
(1, 'Black'),
(2, 'Blue'),
(3, 'Green');

-- --------------------------------------------------------

--
-- Table structure for table `contactus`
--

CREATE TABLE IF NOT EXISTS `contactus` (
  `contactus_id` int(11) NOT NULL AUTO_INCREMENT,
  `type` varchar(255) DEFAULT NULL,
  `contact` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`contactus_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `contactus`
--

INSERT INTO `contactus` (`contactus_id`, `type`, `contact`) VALUES
(1, 'HP', '085225123456'),
(2, 'BB', 'g469akr4'),
(3, 'e-mail', 'cs@yaris.biz');

-- --------------------------------------------------------

--
-- Table structure for table `featured_product`
--

CREATE TABLE IF NOT EXISTS `featured_product` (
  `featured_id` int(11) NOT NULL AUTO_INCREMENT,
  `item_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`featured_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `featured_product`
--

INSERT INTO `featured_product` (`featured_id`, `item_id`) VALUES
(4, 5),
(5, 3);

-- --------------------------------------------------------

--
-- Table structure for table `how_to_order`
--

CREATE TABLE IF NOT EXISTS `how_to_order` (
  `how_to_order_id` int(11) NOT NULL AUTO_INCREMENT,
  `content` text,
  PRIMARY KEY (`how_to_order_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `how_to_order`
--

INSERT INTO `how_to_order` (`how_to_order_id`, `content`) VALUES
(2, '<p>\r\n	Satu</p>\r\n'),
(3, '<p>\r\n	Dua</p>\r\n'),
(4, '<p>\r\n	Tiga</p>\r\n');

-- --------------------------------------------------------

--
-- Table structure for table `item`
--

CREATE TABLE IF NOT EXISTS `item` (
  `item_id` int(11) NOT NULL AUTO_INCREMENT,
  `item` varchar(255) DEFAULT NULL,
  `price` int(11) DEFAULT NULL,
  `point` varchar(255) DEFAULT NULL,
  `subcategory_id` int(11) DEFAULT NULL,
  `date` date DEFAULT NULL,
  `description` text,
  `total_stock` int(11) DEFAULT NULL,
  `status` enum('new arrival','featured','ordinary') DEFAULT 'new arrival',
  PRIMARY KEY (`item_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Dumping data for table `item`
--

INSERT INTO `item` (`item_id`, `item`, `price`, `point`, `subcategory_id`, `date`, `description`, `total_stock`, `status`) VALUES
(1, 'Abc', 2000, '5', 1, '2012-09-26', '<p>\r\n	Lorem Ipsum adalah contoh teks atau dummy dalam industri percetakan dan penataan huruf atau typesetting. Lorem Ipsum telah menjadi standar contoh teks sejak tahun 1500an, saat seorang tukang cetak yang tidak dikenal mengambil sebuah kumpulan teks dan mengacaknya untuk menjadi sebuah buku contoh huruf. Ia tidak hanya bertahan selama 5 abad, tapi juga telah beralih ke penataan huruf elektronik, tanpa ada perubahan apapun. Ia mulai dipopulerkan pada tahun 1960 dengan diluncurkannya lembaran-lembaran Letraset yang menggunakan kalimat-kalimat dari Lorem Ipsum, dan seiring munculnya perangkat lunak Desktop Publishing seperti Aldus PageMaker juga memiliki versi Lorem Ipsum.</p>\r\n', 4, 'new arrival'),
(2, 'Qwer', 3500, '5', 1, '2012-09-29', '<p>\r\n	Asal</p>\r\n', 20, 'new arrival'),
(3, 'Asdf', 4000, '10', 1, '2012-09-29', '<p>\r\n	xfghsfgadfdsfsdf</p>\r\n', 50, 'new arrival'),
(5, 'Pdgh', 5000, '4', 1, '2012-09-29', '<p>\r\n	djsfghsgfg</p>\r\n', 50, 'new arrival'),
(6, 'My Dummy', 40000, '5', 1, '2012-10-26', '<p>\r\n	Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi. Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum. Typi non habent claritatem insitam; est usus legentis in iis qui facit eorum claritatem. Investigationes demonstraverunt lectores legere me lius quod ii legunt saepius. Claritas est etiam processus dynamicus, qui sequitur mutationem consuetudium lectorum. Mirum est notare quam littera gothica, quam nunc putamus parum claram, anteposuerit litterarum formas humanitatis per seacula quarta decima et quinta decima. Eodem modo typi, qui nunc nobis videntur parum clari, fiant sollemnes in futurum.</p>\r\n', 40, 'new arrival');

-- --------------------------------------------------------

--
-- Table structure for table `message`
--

CREATE TABLE IF NOT EXISTS `message` (
  `message_id` int(11) NOT NULL AUTO_INCREMENT,
  `subject` varchar(255) DEFAULT NULL,
  `message` text,
  `username` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`message_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `message`
--

INSERT INTO `message` (`message_id`, `subject`, `message`, `username`, `email`) VALUES
(1, 'test', 'asdfasdfasdfasdf', 'tetchanz', 'tetchanz@yahoo.com'),
(2, 'test 2', 'gklhjirqoeroqierjialdf', 'N/A', 'asdf@yahoo.com'),
(3, 'test 3', 'dsfghkdlfgjhk', 'N/A', 'qwer@yahoo.com');

-- --------------------------------------------------------

--
-- Table structure for table `news`
--

CREATE TABLE IF NOT EXISTS `news` (
  `news_id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) DEFAULT NULL,
  `news` text,
  `user_id` int(11) DEFAULT NULL,
  `status` enum('show','not show') DEFAULT NULL,
  `date` date NOT NULL,
  `picture` varchar(255) NOT NULL,
  PRIMARY KEY (`news_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `news`
--

INSERT INTO `news` (`news_id`, `title`, `news`, `user_id`, `status`, `date`, `picture`) VALUES
(1, 'Test', '<p>Lorem Ipsum adalah contoh teks atau dummy dalam industri percetakan dan penataan huruf atau typesetting. Lorem Ipsum telah menjadi standar contoh teks sejak tahun 1500an, saat seorang tukang cetak yang tidak dikenal<strong> </strong></p>', NULL, 'show', '2012-09-27', 'f0f62-ffx-lse00-1024-1-.jpg'),
(2, 'Dua', '<p>Lorem <strong>Ipsum adalah</strong> contoh <span style="color: #ff0000;">teks atau dummy</span> dalam industri percetakan dan penataan huruf atau typesetting. Lorem Ipsum telah menjadi standar contoh teks sejak tahun 1500an, saat seorang tukang cetak yang tidak dikenal</p>', NULL, 'show', '2012-09-26', '38049-finalx_13_1024.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `new_arrival`
--

CREATE TABLE IF NOT EXISTS `new_arrival` (
  `new_arrival_id` int(11) NOT NULL AUTO_INCREMENT,
  `item_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`new_arrival_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `new_arrival`
--

INSERT INTO `new_arrival` (`new_arrival_id`, `item_id`) VALUES
(1, 1),
(2, 5),
(3, 3),
(4, 2),
(5, 6);

-- --------------------------------------------------------

--
-- Table structure for table `payment`
--

CREATE TABLE IF NOT EXISTS `payment` (
  `payment_id` int(11) NOT NULL AUTO_INCREMENT,
  `cart_id` int(11) DEFAULT NULL,
  `date` date DEFAULT NULL,
  `amount_paid` int(11) DEFAULT NULL,
  `bank_id` int(11) DEFAULT NULL,
  `account_name` varchar(255) DEFAULT NULL,
  `payment_account` varchar(45) DEFAULT NULL,
  `payment_status` enum('Unpaid','Paid','Verified','Rejected','Sent') DEFAULT 'Paid',
  PRIMARY KEY (`payment_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=13 ;

--
-- Dumping data for table `payment`
--

INSERT INTO `payment` (`payment_id`, `cart_id`, `date`, `amount_paid`, `bank_id`, `account_name`, `payment_account`, `payment_status`) VALUES
(1, 8, '2012-10-06', 6000, 2147483647, NULL, '5270565656', 'Rejected'),
(2, 7, '2012-10-06', 6000, 2147483647, NULL, '5280000000', 'Sent'),
(3, 6, '2012-10-06', 24000, 2147483647, NULL, '5270123456', 'Rejected'),
(4, 11, '2012-10-06', 4000, 2147483647, NULL, '5270123456', 'Sent'),
(5, 10, '2012-10-06', 4000, 2147483647, NULL, '5321000000', 'Sent'),
(6, 5, '2012-10-07', 10000, 1, NULL, '50000', 'Sent'),
(7, 9, '2012-10-07', 6000, 1, NULL, '5280000000', 'Sent'),
(8, 12, '2012-10-18', 4000, 1, 'account_name', '3453', 'Paid'),
(11, 8, '2012-10-18', 4000, 1, 'aasd', '5280000000', 'Paid'),
(12, 13, '2012-10-01', 8000, 1, 'qwer', '5280000000', 'Paid');

-- --------------------------------------------------------

--
-- Table structure for table `pictures`
--

CREATE TABLE IF NOT EXISTS `pictures` (
  `picture_id` int(11) NOT NULL AUTO_INCREMENT,
  `picture` varchar(255) DEFAULT NULL,
  `item_id` int(11) DEFAULT NULL,
  `status` int(11) DEFAULT NULL,
  `title` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`picture_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=21 ;

--
-- Dumping data for table `pictures`
--

INSERT INTO `pictures` (`picture_id`, `picture`, `item_id`, `status`, `title`) VALUES
(1, 'ca5d-1_480270992l.jpg', 1, 2, 'satu'),
(2, 'f4e0-1_422058814l.jpg', 1, 3, 'tiga'),
(3, '4d2a-1_430068504l.jpg', 1, 5, 'dua'),
(4, '2f3f-f9069986f068593466096e33.jpg', 2, 1, ''),
(5, '2932-yuffie_blue.jpg', 2, 2, NULL),
(6, 'df36-c4e333dfea3f2f1949540381.jpg', 2, 3, NULL),
(7, '1a6d-ffx2-800.jpg', 3, 1, NULL),
(8, 'fcb1-ffx-lse00-1024[1].jpg', 3, 2, NULL),
(9, '3013-ffx-yuna4.jpg', 3, 3, NULL),
(10, '81d9-finalx_03_1024.jpg', 3, 4, NULL),
(11, '33e7-wall04.jpg', 5, 1, NULL),
(12, '6d9f-wall15.jpg', 5, 2, NULL),
(13, 'deaf-doasinfinitygalleries.jpg', 5, 3, NULL),
(14, 'b761-wall06.jpg', 5, 4, NULL),
(15, '6924-b.jpg', 1, 1, 'lima'),
(17, 'd4ef-117680754912716126080.jpg', 1, 4, 'empat'),
(18, '1faa-zack-fair.jpg', 6, 2, 'zxcv'),
(19, '6136-zack.jpg', 6, 1, 'qwer'),
(20, '0fa4-final-fantasy-vii-top-10-characters-20080325112132151.jpg', 6, 3, 'asdf');

-- --------------------------------------------------------

--
-- Table structure for table `shipment`
--

CREATE TABLE IF NOT EXISTS `shipment` (
  `shipment_id` int(11) NOT NULL,
  `cart_id` int(11) DEFAULT NULL,
  `status` enum('delivery','delivered') DEFAULT NULL,
  `shipment_cost` int(11) DEFAULT NULL,
  PRIMARY KEY (`shipment_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `size`
--

CREATE TABLE IF NOT EXISTS `size` (
  `size_id` int(11) NOT NULL AUTO_INCREMENT,
  `size` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`size_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `size`
--

INSERT INTO `size` (`size_id`, `size`) VALUES
(1, 'S'),
(2, 'M'),
(3, 'L'),
(4, 'XL');

-- --------------------------------------------------------

--
-- Table structure for table `subcategory`
--

CREATE TABLE IF NOT EXISTS `subcategory` (
  `subcategory_id` int(11) NOT NULL AUTO_INCREMENT,
  `subcategory` varchar(255) DEFAULT NULL,
  `category_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`subcategory_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `subcategory`
--

INSERT INTO `subcategory` (`subcategory_id`, `subcategory`, `category_id`) VALUES
(1, 'Kaos', 2),
(2, 'Kaos', 1),
(3, 'Celana', 1);

-- --------------------------------------------------------

--
-- Table structure for table `term_of_service`
--

CREATE TABLE IF NOT EXISTS `term_of_service` (
  `term_of_service_id` int(11) NOT NULL AUTO_INCREMENT,
  `content` text,
  PRIMARY KEY (`term_of_service_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `term_of_service`
--

INSERT INTO `term_of_service` (`term_of_service_id`, `content`) VALUES
(1, '<p>\r\n	Yi</p>\r\n'),
(2, '<p>\r\n	Er</p>\r\n'),
(3, '<p>\r\n	San</p>\r\n'),
(4, '<p>\r\n	Si</p>\r\n'),
(5, '<p>\r\n	Wu</p>\r\n');

-- --------------------------------------------------------

--
-- Table structure for table `tr_color`
--

CREATE TABLE IF NOT EXISTS `tr_color` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `item_id` int(11) DEFAULT NULL,
  `color_id` int(11) DEFAULT NULL,
  `remark` varchar(45) DEFAULT NULL,
  `stock` int(11) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=15 ;

--
-- Dumping data for table `tr_color`
--

INSERT INTO `tr_color` (`id`, `item_id`, `color_id`, `remark`, `stock`) VALUES
(1, 1, 1, '0', 2),
(2, 1, 2, '1', 2),
(4, 5, 1, '0', 4),
(5, 5, 2, '1', 3),
(6, 5, 3, '2', 5),
(7, 2, 1, '0', 0),
(8, 2, 2, '1', 0),
(9, 2, 3, '2', 0),
(10, 3, 1, '0', 0),
(11, 3, 2, '1', 0),
(12, 3, 3, '2', 0),
(13, 6, 1, '0', 18),
(14, 6, 2, '1', 15);

-- --------------------------------------------------------

--
-- Table structure for table `tr_size`
--

CREATE TABLE IF NOT EXISTS `tr_size` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `item_id` int(11) DEFAULT NULL,
  `size_id` int(11) DEFAULT NULL,
  `remark` varchar(45) DEFAULT NULL,
  `stock` int(11) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=12 ;

--
-- Dumping data for table `tr_size`
--

INSERT INTO `tr_size` (`id`, `item_id`, `size_id`, `remark`, `stock`) VALUES
(1, 1, 3, '0', 4),
(3, 5, 3, '0', 4),
(4, 5, 2, '1', 0),
(5, 5, 1, '2', 0),
(6, 2, 3, '0', 7),
(7, 2, 2, '1', 9),
(8, 2, 1, '2', 4),
(9, 3, 3, '0', 0),
(10, 3, 2, '1', 0),
(11, 3, 1, '2', 0);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `user_type_id` int(11) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `birthday` date DEFAULT NULL,
  `gender` varchar(45) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `total_point` int(11) DEFAULT '0',
  `city` varchar(255) DEFAULT NULL,
  `phone` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=11 ;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`user_id`, `username`, `password`, `user_type_id`, `email`, `birthday`, `gender`, `address`, `total_point`, `city`, `phone`) VALUES
(1, 'admin', '21232f297a57a5a743894a0e4a801fc3', 1, NULL, NULL, NULL, NULL, 0, NULL, NULL),
(2, 'tetchanz', '962012d09b8170d912f0669f6d7d9d07', NULL, 'tetchanz@yahoo.com', '1988-10-08', '', 'Jl. Mandala 4\nSukabumi Utara\nKebon Jeruk', 410, 'Jakarta Barat', '085123456'),
(3, 'ryu_hydetetsu', '912ec803b2ce49e4a541068d495ab570', NULL, 'asdf', '2012-10-02', '', NULL, 0, NULL, NULL),
(9, 'halohalo', '912ec803b2ce49e4a541068d495ab570', NULL, 'asdf', '2012-10-02', '', NULL, 0, NULL, NULL),
(10, 'cobacoba', '912ec803b2ce49e4a541068d495ab570', NULL, 'asdf', '2012-10-02', '', NULL, 0, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `user_type`
--

CREATE TABLE IF NOT EXISTS `user_type` (
  `user_type_id` int(11) NOT NULL,
  `user_type` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`user_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `welcome_page`
--

CREATE TABLE IF NOT EXISTS `welcome_page` (
  `welcome_id` int(11) NOT NULL AUTO_INCREMENT,
  `content` text,
  PRIMARY KEY (`welcome_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Dumping data for table `welcome_page`
--

INSERT INTO `welcome_page` (`welcome_id`, `content`) VALUES
(1, '<p style="text-align: justify; ">\r\n	<span style="color: rgb(0, 0, 0); font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 10px; line-height: 17px; text-align: justify; ">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi. Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum. Typi non habent claritatem insitam; est usus legentis in iis qui facit eorum claritatem. Investigationes demonstraverunt lectores legere me lius quod ii legunt saepius. Claritas est etiam processus dynamicus, qui sequitur mutationem consuetudium lectorum. Mirum est notare quam littera gothica, quam nunc putamus parum claram, anteposuerit litterarum formas humanitatis per seacula quarta decima et quinta decima. Eodem modo typi, qui nunc nobis videntur parum clari, fiant sollemnes in futurum.</span></p>\r\n');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
