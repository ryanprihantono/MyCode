<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class User_model extends CI_Model {

	public function __construct(){
		parent::__construct();
	}
	public function register($username,$password,$email,$gender,$birthday,$address,$city,$phone){
		$query = $this->db->query("insert into user (username,password,email,gender,birthday,address,city,phone) values ('$username','$password','$email','$gender','$birthday','$address','$city','$phone')");
		return $query;
	}
	
	public function cek_username($username){
		$query = $this->db->query("select * from user where username='$username'");
		if($query->num_rows()>0){
			return true;
		}
		else{
			return false;
		}
	}
	public function login($username){
		$query = $this->db->query("select * from user where username='$username'");
		return $query->first_row();
	}
	public function get_contactus(){
		$query = $this->db->query("select * from contactus");
		return $query;
	}
	
	public function get_userdata($user_id){
		$query = $this->db->query("select * from user where user_id=$user_id");
		return $query->first_row();
	}
	public function edit_profile($user_id,$action,$value){
		$query = $this->db->query("update user set $action = '$value' where user_id=$user_id");
		return $query;
	}
	public function contactus_message($username,$email,$subject,$message){
		$query = $this->db->query("insert into message (username,email,subject,message) values ('$username','$email','$subject','$message')");
		return $query;
	}
	public function get_welcome(){
		$query = $this->db->query("select * from welcome_page");
		$page = "";
		foreach($query->result() as $row){
			$page .="<div>$row->content</div>";
		}
		return $page;
	}
}