<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Barangmodel extends CI_Model {

	public function __construct(){
		parent::__construct();
	}
	
	public function lastid(){
		$result = $this->db->query("SELECT * FROM barang ORDER BY barang.id_barang DESC LIMIT 0 , 1");
		return $result;
	}
	public function lastid_pembelian(){
		$result = $this->db->query("SELECT * FROM pengalihan ORDER BY barang.id_pengalihan DESC LIMIT 0 , 1");
		return $result;
	}
	public function get_nama_barang($id){
		$result = $this->db->query("select * from barang where id_barang=$id");
		$namabarang='';
		foreach ($result->result() as $row) {
			$namabarang = $row->nama_barang;
		}
		return $namabarang;
	}
	public function get_tr_pengalihan($id_barang){
		$query = $this->db->query("select * from tr_pengalihan where id_barang=$id_barang");
		return $query->num_rows();
	}
	public function get_tr_perbaikan($id_barang){
		$query = $this->db->query("select * from tr_perbaikan where id_barang=$id_barang");
		return $query->num_rows();
	}
	public function history_pengalihan($id){
		$query = $this->db->query("select distinct * from tr_pengalihan join pengalihan on tr_pengalihan.id_pengalihan=pengalihan.id_pengalihan join barang on barang.id_barang=tr_pengalihan.id_barang where tr_pengalihan.id_barang = $id");
		return $query;
	}
	public function history_perbaikan($id){
		$query = $this->db->query("select distinct * from tr_perbaikan join perbaikan on tr_perbaikan.id_perbaikan=perbaikan.id_perbaikan join barang on barang.id_barang=tr_perbaikan.id_barang where tr_perbaikan.id_barang = $id");
		return $query;
	}
	
}