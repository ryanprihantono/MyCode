<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Ruangmodel extends CI_Model {

	public function __construct(){
		parent::__construct();
	}
	
	public function get_ruang($id_ruang){
		$result = $this->db->query("SELECT * FROM ruangan where id_ruang=$id_ruang");
		return $result->first_row();
	}
	public function get_barang($id){
		$query = $this->db->query("select * from barang where id_ruang=$id");
		return $query;
	}
	public function get_kategori_ruang($id_kategori_ruang){
		$result = $this->db->query("SELECT * FROM kategori_ruang where id_kategori_ruang=$id_kategori_ruang");
		return $result->first_row();
	}

}