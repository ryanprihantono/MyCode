<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Product_model extends CI_Model {

	public function __construct(){
		parent::__construct();
	}
	
	public function get_product($subcategory_id=NULL,$key=NULL){
		$string = "SELECT * FROM item join pictures on item.item_id=pictures.item_id where pictures.status=1";
		if($subcategory_id!=NULL){
			$string = "SELECT * FROM item join pictures on item.item_id=pictures.item_id where pictures.status=1 and subcategory_id=$subcategory_id";
		}
		if($subcategory_id==NULL && $key!=NULL){
			$string = "SELECT * FROM item join pictures on item.item_id=pictures.item_id where pictures.status=1 and item like '%$key%'";
			echo $string;
		}
		$query = $this->db->query($string);
		$product = array();
		$i=0;
		foreach($query->result() as $row){
			$size = $this->get_size($row->item_id);
			$flag = false;
			foreach($size->result() as $row1){
				if($row1->stock>0){
					$flag = true;
				}
			}
			$color = $this->get_color($row->item_id);
			$flag1 = false;
			foreach($color->result() as $row1){
				if($row1->stock>0){
					$flag1 = true;
				}
			}
			if($flag1==true){
				$product[$i] = $row;
				$i++;
			}
		}
		return $product;
	}
	public function get_title($subcategory_id=NULL,$key=NULL){
		$query = NULL;
		if($subcategory_id!=NULL){
			$query = $this->db->query("select * from subcategory join category on subcategory.category_id=category.category_id where subcategory_id=$subcategory_id");
			$row = $query->first_row();
			return "Product - ".$row->subcategory." - ".$row->category;
		}
		else{
			return "Search Product - ".$key;
		}
		
	}
	public function get_categories(){
		$table='';
		$query = $this->db->query("select * from category");
		foreach($query->result() as $row){
			$table .=  "<h3><a href='#'>$row->category</a></h3><ul>";
			$subcategory = $this->get_subcategories($row->category_id);
			foreach($subcategory->result() as $sub){
				$table .= "<li><a href='".base_url()."main/product/".$sub->subcategory_id."'>$sub->subcategory</a></li>";
			}
			$table .= "</ul>";
		}
		return $table;
	}
	public function get_subcategories($category_id){
		$query = $this->db->query("select * from subcategory where category_id=$category_id");
		return $query;
	}
	public function get_new_arrival(){
		$query = $this->db->query("SELECT * FROM new_arrival join item on new_arrival.item_id=item.item_id join pictures on item.item_id=pictures.item_id where pictures.status=1");
		$product = array();
		$i=0;
		foreach($query->result() as $row){
			$product[$i] = $row;
			$i++;
		}
		return $product;
	}
	public function get_new_product(){
		$query = $this->db->query("SELECT * FROM new_arrival join item on new_arrival.item_id=item.item_id join pictures on item.item_id=pictures.item_id where pictures.status=1");
		$product = array();
		$i=0;
		foreach($query->result() as $row){
			$product[$i] = $row;
			$i++;
		}
		return $product;
	}
	public function get_featured_product(){
		$query = $this->db->query("SELECT * FROM featured_product join item on featured_product.item_id=item.item_id join pictures on item.item_id=pictures.item_id where pictures.status=1");
		$product = array();
		$i=0;
		foreach($query->result() as $row){
			$product[$i] = $row;
			$i++;
		}
		return $product;
	}
	public function get_product_row($id){
		$query = $this->db->query("SELECT * FROM item where item_id=$id");
		return $query->first_row();
	}
	public function get_pictures($item_id){
		$query = $this->db->query("select * from pictures where item_id=$item_id order by pictures.status");
		return $query;
	}
	public function get_news($news_id=NULL){
		$string = "SELECT * FROM news where status='show'";
		
		if($news_id!=NULL)
			$string = "SELECT * FROM news where news_id=$news_id";
		$query = $this->db->query($string);
		return $query;
	}
	public function get_how_to_order(){
		$query = $this->db->query("SELECT * FROM how_to_order");
		return $query;
	}
	public function get_term_of_service(){
		$query = $this->db->query("SELECT * FROM term_of_service");
		return $query;
	}
	public function get_color($item_id){
		$query = $this->db->query("select * from tr_color join color on color.color_id = tr_color.color_id where item_id=$item_id and stock <> 0 or stock <> NULL");
		return $query;
	}
	public function get_size($item_id){
		$query = $this->db->query("select * from tr_size join size on size.size_id = tr_size.size_id where item_id=$item_id and stock <> 0 or stock <> NULL");
		return $query;
	}
	public function get_size_name($size_id){
		$query = $this->db->query("select * from size where size_id=$size_id");
		return $query->first_row()->size;
	}
	public function get_color_name($color_id){
		$query = $this->db->query("select * from color where color_id=$color_id");
		return $query->first_row()->color;
	}
	public function get_table($item_id,$tablename){
		$query = $this->db->query("select * from item join tr_$tablename on item.item_id=tr_$tablename.item_id join $tablename on $tablename.".$tablename."_id = tr_$tablename.".$tablename."_id where item.item_id=$item_id");
		return $query;
	}
	public function update_stock($tablename,$id,$stock){
		$query = $this->db->query("update tr_$tablename set stock=$stock where id=$id");
		return $query;
	}
	public function insert_cart($user_id,$date,$billcode,$remark,$total,$cart){
		$query = $this->db->query("insert into cart (user_id,date,remark,bill_code,total_bill) values($user_id,'$date','$remark','$billcode',$total)");
		if($query){
			$query = $this->db->query("select * from cart where user_id=$user_id and date='$date' order by cart_id desc");
			$last_cart = $query->first_row();
			
			foreach($cart as $row){
				$query = $this->db->query("insert into cart_detail (cart_id,item_id,color_id,qty) values($last_cart->cart_id,".$row['item_id'].",".$row['color_id'].",".$row['qty'].")");
			}
		}
		$this->update_point($user_id,$cart);
		return $query;
	}
	public function update_point($user_id,$cart){
		$query = $this->db->query("select * from user where user_id=$user_id");
		$curr = $query->first_row()->total_point;
		
		foreach($cart as $row){
			$curr += ($row['point']*$row['qty']);
		}
		$this->db->query("update user set total_point=$curr where user_id=$user_id");
	}
	public function autosearch($key){
		$query = $this->db->query("select item from item where item like '%$key%'");
		$tags = array();
		$i=0;
		foreach($query->result() as $row){
			$tags[$i] = $row->item;
			$i++;
		}
		return $tags;
	}
}