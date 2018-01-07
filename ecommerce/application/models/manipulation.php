<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Manipulasi extends CI_Model{
	
	public function input($data,$table){
		$this->db->insert($table,$data);
		return $this->db->insert_id();
	}
	
	public function inputDetail($data,$table,$header_id){
		$this->db->insert($table,$data);
		return $header_id;
	}
	
	public function edit($data,$table,$pk,$key){
		$this->db->where($pk,$key);
		$this->db->update($table,$data);
	}
	
	public function erase($table,$pk,$key){
		$this->db->where($pk,$key);
		$this->db->delete($table);
		return $key;
	}
}