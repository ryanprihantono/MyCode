<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class Application extends CI_Controller {
	var $id_barang = 0;
	var $id_ruang = 0;
	var $asal = "";
	var $tujuan = "";
	public function __construct(){
		parent::__construct();
		$this->load->model("barangmodel",'barang');
		$this->load->model("ruangmodel",'ruang');	
	}
	
	public function output($output = null){
		if($this->session->userdata('username')!='' && $this->session->userdata('username')!= NULL){
			$this->load->view("default",$output);
		}
		else{
			$this->load->view("login",$output);
		}
	}
	
	public function index(){
		if($this->session->userdata('login')==false){
			redirect('login');
		}else{
			$data = array(
				"web_title"=>"UKSW Inventory",
				"navigasi"=>"menu_utama",
				"sidebar"=>"sidebar_utama",
				"page_name"=>"view",
				"page_detail"=>"dashboard_emp",
				"page_title"=>"Home",
				"dashboard"=>"active",
				"report"=>""
			);
			$this->load->view('default',$data);
		}
	}
	
	public function kategori_barang(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('kategori_brg');
			$crud->set_subject('Kategori Barang');
			$crud->required_fields('kategori_brg');
			$crud->columns('id_kategori','kategori_brg');
			$crud->display_as('kategori_brg','Kategori Barang');
			$output = $crud->render();
			$data = array(
				"web_title"=>"Kategori Barang",
				"page_title"=>"Kategori Barang",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	public function barang(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('barang');
			$crud->set_subject('Barang');
			
			$crud->required_fields('nama_barang');
			$crud->columns('id_barang','nama_barang','id_kategori','merk',"spesifikasi","kondisi","id_ruang");
			$crud->set_relation('id_kategori','kategori_brg','kategori_brg');
			$crud->set_relation('id_ruang','ruangan','nama_ruang');
			$crud->display_as('id_kategori','Kategori Barang');
			$crud->display_as('id_ruang','Lokasi');
			
			$crud->callback_before_update(array($this,'before_pengalihan_callback'));
			$crud->callback_after_update(array($this,'after_pengalihan_callback'));
			//$crud->add_action('Pengalihan Barang', '', 'application/barang','ui-icon-check');
			//$crud->add_action('Perbaikan Barang', '', 'application/barang','ui-icon-check');
			//$crud->callback_field('nama_ruang',array($this,'field_callback_1'));
			//$crud->callback_add_field('id_barang',array($this,'gen_id_barang'));
			$output = $crud->render();
			$data = array(
				"web_title"=>"Barang",
				"page_title"=>"Barang",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function before_pengalihan_callback($post_array, $primary_key){
		if(!empty($post_array['id_ruang'])){
			$query = $this->db->query("select * from barang where id_barang=$primary_key");
			foreach($query->result() as $row){
				$this->asal=$row->id_ruang;
			}
		}
		return $post_array;
	}
	public function after_pengalihan_callback($post_array, $primary_key){
		if(!empty($post_array['id_ruang'])){
			$this->tujuan = $post_array['id_ruang'];
		}
		$now = date("Y-m-d");
		$this->db->query("insert into pengalihan (lokasi_asal,lokasi_tujuan,tanggal) values ('$this->asal','$this->tujuan','$now')");
		$query = $this->db->query("SELECT * FROM pengalihan ORDER BY id_pengalihan DESC LIMIT 0,1");
		$id_pengalihan = 0;
		foreach($query->result() as $row){
			$id_pengalihan = $row->id_pengalihan;
		}
		$this->db->query("insert into tr_pengalihan values($primary_key,$id_pengalihan,'0')");
		return $post_array;
	}
	public function history_barang(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('datatables');
			$crud->set_table('barang');
			$crud->set_subject('Barang');
			$crud->required_fields('nama_barang');
			$crud->columns('id_barang','nama_barang','id_kategori','merk',"spesifikasi","kondisi","id_ruang");
			$crud->set_relation('id_kategori','kategori_brg','kategori_brg');
			$crud->set_relation('id_ruang','ruangan','nama_ruang');
			$crud->display_as('id_kategori','Kategori Barang');
			$crud->display_as('id_ruang','Lokasi');
			$crud->unset_add();
			$crud->unset_edit();
			$crud->unset_delete();
			$crud->add_action('History Pengalihan', '', 'application/history_pengalihan','ui-icon-view');
			$crud->add_action('History Perbaikan', '', 'application/history_perbaikan','ui-icon-view');
			$output = $crud->render();
			$data = array(
				"web_title"=>"Barang",
				"page_title"=>"Barang",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function history_pengalihan($id){
		try{
			$this->id_barang = $id;
			
			//temporary using ordinary table
			$this->load->model("barangmodel","mod");
			$result = $this->mod->history_pengalihan($id);
			$total = $result->num_rows();
			
			$table='';
			
			$table .= "<table align='center'><tr><td width='200'>ID Barang</td><td width='300'>Nama Barang</td><td width='300'>Tanggal</td><td width='200'>Lokasi Asal</td><td width='200'>Lokasi Tujuan</td></tr>";
			foreach ($result->result() as $row) {
				$table .= "<tr><td width='200'>".$row->id_barang."</td><td width='300'>".$row->nama_barang."</td><td width='300'>".$row->tanggal."</td><td width='200'>".$this->ruang->get_ruang($row->lokasi_asal)->nama_ruang."</td><td width='200'>".$this->ruang->get_ruang($row->lokasi_tujuan)->nama_ruang."</td></tr>";
			}
			$table .= "</table>";
			$data = array(
				"web_title"=>"History Pengalihan",
				"page_title"=>"History Pengalihan",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"table",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$data['table']= $table;
			$output = $data;
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function history_perbaikan($id){
		try{
			$this->id_barang = $id;
			
			//temporary using ordinary table
			$this->load->model("barangmodel","mod");
			$result = $this->mod->history_perbaikan($id);
			$total = $result->num_rows();
			
			$table='';
			
			$table .= "<table align='center'><tr><td width='200'><b>ID Barang</b></td><td width='400'><b>Nama Barang</b></td><td width='300'><b>Tanggal</b></td><td width='300'><b>Keterangan Kerusakan</b></td></tr>";
			foreach ($result->result() as $row) {
				$table .= "<tr><td width='200'>".$row->id_barang."<td width='400'>".$row->nama_barang."</td><td width='300'>".$row->tanggal."</td><td width='300'>".$row->keterangan_kerusakan."</td></tr>";
			}
			$table .= "</table>";
			$data = array(
				"web_title"=>"History Perbaikan",
				"page_title"=>"History Perbaikan",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"table",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$data['table']= $table;
			$output = $data;
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function get_history_pengalihan(){
		$this->load->model("barangmodel","mod");
		$result = $this->mod->history_pengalihan($this->id_barang);
		$page = 10;
		$total = $result->num_rows(); 
		$json = "";
		$json .= "{\n";
		$json .= "page: $page,\n";
		$json .= "total: $total,\n";
		$json .= "rows: [";
		$rc = false;
		
		foreach ($result->result() as $row) {
		//if ($rc) $json .= ",";
		$json .= "\n{";
		$json .= "id:'".$row->id_barang."',";
		$json .= "cell:['".$row->id_barang."','".$row->nama_barang."'";
		$json .= ",'".addslashes($row->tanggal)."'";
		$json .= ",'".addslashes($row->lokasi_asal)."'";
		$json .= ",'".addslashes($row->lokasi_tujuan)."'";
		$json .= "}";
		$rc = true;
		}
		$json .= "]\n";
		$json .= "}";
		echo $json;
	}
	
	public function gen_id_barang(){
		$result = $this->barangmodel->lastid();
		$id=0;
		foreach($result->result() as $row)
			$id = $row->id_barang+1;
		return "<input type='text' readonly='readonly' id='id_barang' value='$id'>";
	}
	public function pembelian(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('barang');
			$crud->set_subject('Pembelian Barang');
			$crud->required_fields('id_barang');
			
			$crud->set_relation('id_kategori','kategori_brg','kategori_brg');
			//$crud->callback_add_field('id_pembelian',array($this,'gen_id_pembelian'));
			$crud->columns('id_barang','nama_barang','tanggal_pembelian','merk','spesifikasi','harga');
			
			$crud->display_as('id_kategori','Kategori Barang');
			$output = $crud->render();
			$data = array(
				"web_title"=>"Pembelian Barang",
				"page_title"=>"Pembelian Barang",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function gen_id_pembelian(){
		$result = $this->barangmodel->lastid_pembelian();
		$id=0;
		foreach($result->result() as $row)
			$id = $row->id_pengalihan+1;
		return "<input type='text' readonly='readonly' id='id_pembelian' value='$id'>";
	}
	public function pengalihan(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('pengalihan');
			$crud->set_subject('Pengalihan Barang');
			//$crud->set_relation('id_barang','barang','{id_barang}-{nama_barang}');
			$crud->set_relation('lokasi_asal','ruangan','nama_ruang');
			$crud->set_relation('lokasi_tujuan','ruangan','nama_ruang');
			//$crud->required_fields('id_barang');
			$crud->set_relation_n_n('Barang','tr_pengalihan','barang','id_pengalihan','id_barang','nama_barang','remark');
			
			//$crud->display_as('id_barang','ID Barang - Nama Barang');
			$crud->columns('id_pengalihan','lokasi_asal','lokasi_tujuan','tanggal','Barang');
			
			//$crud->display_as('id_ruang','Lokasi Tujuan');
			$output = $crud->render();
			$data = array(
				"web_title"=>"Pengalihan Barang",
				"page_title"=>"Pengalihan Barang",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	
	public function perbaikan(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('perbaikan');
			$crud->set_subject('Perbaikan Barang');
			$crud->required_fields('id_barang');
			//$crud->set_relation('id_barang','barang','{id_barang} - {nama_barang}');
			
			$crud->set_relation_n_n('Barang','tr_perbaikan','barang','id_perbaikan','id_barang','nama_barang','remark');
			
			//$crud->display_as('id_barang','ID Barang - Nama Barang');
			//$crud->columns('id_perbaikan','nama_barang','keterangan_kerusakan','tanggal');
			$output = $crud->render();
			$data = array(
				"web_title"=>"Perbaikan Barang",
				"page_title"=>"Perbaikan Barang",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	
	public function ruangan(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('ruangan');
			$crud->set_subject('Ruangan');
			$crud->required_fields('id_ruang','nama_ruang');
			$crud->set_relation('id_kategori_ruang','kategori_ruang','kategori_ruang');
			$crud->display_as('id_kategori_ruang','Kategori Ruang');
			$crud->columns('id_ruang','nama_ruang','id_kategori_ruang','gedung');
			$output = $crud->render();
			$data = array(
				"web_title"=>"Ruangan",
				"page_title"=>"Ruangan",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function kategori_ruangan(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('kategori_ruang');
			$crud->set_subject('Kategori Ruang');
			$crud->required_fields('kategori_ruang');
			$crud->columns('id_kategori_ruang','kategori_ruang');
			//$crud->display_as('kategori_brg','Kategori Barang');
			$output = $crud->render();
			$data = array(
				"web_title"=>"Kategori Barang",
				"page_title"=>"Kategori Barang",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function dosen(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('dosen');
			$crud->set_subject('Dosen');
			$crud->required_fields('nama_dosen');
			$crud->columns('id_dosen','nama_dosen');
			//$crud->display_as('kategori_brg','Kategori Barang');
			$output = $crud->render();
			$data = array(
				"web_title"=>"Dosen",
				"page_title"=>"Dosen",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function manage_user(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('users');
			$crud->set_subject('Users');
			$crud->required_fields('username','email');
			
			
			$crud->set_relation('id_role','roles','role');
			
			$crud->change_field_type('password', 'hidden');
			$crud->columns('username','email','id_role');
			$crud->display_as('id_role','role');
			//$crud->callback_before_update(array($this,'encrypt_password'));
			$crud->callback_before_insert(array($this,'encrypt_password'));
			$output = $crud->render();
			$data = array(
				"web_title"=>"Users",
				"page_title"=>"Users",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function encrypt_password($post_array) {
		if(empty($post_array['password']))
			$post_array['password'] = md5($post_array['username']);
	  	return $post_array;
	}
	public function roles(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->set_table('roles');
			$crud->set_subject('Roles');
			$crud->required_fields('role');
			$crud->set_relation_n_n('Accessible_pages','tr_page','pages','id_role','id_page','page','remark');
			$crud->columns('role','Accessible_pages');
			$output = $crud->render();
			$data = array(
				"web_title"=>"Roles",
				"page_title"=>"Roles",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function view_laporan(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('datatables');
			$crud->set_table('ruangan');
			$crud->set_subject('Laporan');
			$crud->required_fields('nama_barang');
			$crud->set_relation('id_kategori_ruang','kategori_ruang','kategori_ruang');
			$crud->columns('nama_ruang','id_kategori_ruang','gedung');
			$crud->display_as('id_kategori_ruang','Kategori Ruang');
			//$crud->set_relation('id_kategori','kategori_brg','kategori_brg');
			//$crud->set_relation('id_ruang','ruangan','nama_ruang');
			//$crud->display_as('id_kategori','Kategori Barang');
			//$crud->display_as('id_ruang','Lokasi');
			$crud->unset_add();
			$crud->unset_edit();
			$crud->unset_delete();
			$crud->add_action('View Laporan', '', 'application/laporan','ui-icon-view');
			$output = $crud->render();
			$data = array(
				"web_title"=>"Laporan",
				"page_title"=>"Laporan",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	public function laporan($id){
		try{
			$this->id_ruang = $id;
			
			//temporary using ordinary table
			$this->load->model("ruangmodel","mod");
			
			$ruang = $this->mod->get_ruang($id);
			$barang = $this->mod->get_barang($id);
			
			$table = '';
			$table .= "<table align='center'>
							<tr>
								<td witdh='600'>Ruang</td>
								<td witdh='400'> : ".$ruang->nama_ruang." </td>
							</tr>
							<tr>
								<td witdh='600'>Kategori Ruang</td>
								<td witdh='400'> : ".$this->mod->get_kategori_ruang($ruang->id_kategori_ruang)->kategori_ruang." </td>
							</tr>
							<tr>
								<td witdh='600'>Gedung</td>
								<td witdh='400'> : ".$ruang->gedung." </td>
							</tr>
							<tr>
								<td witdh='600'>Jumlah Barang</td>
								<td witdh='400'> : ".$barang->num_rows()." </td>
							</tr>
						</table><br><br><br>";
			
			$table .= "<table align='center'>
						<th><tr>
							<td width='200'>ID Barang</td>
							<td width='300'>Nama Barang</td>
							<td width='200'>Kondisi</td>
							<td width='200'>Tanggal Pembelian</td>
							<td width='200'>Jumlah Perbaikan</td>
							<td width='200'>Jumlah Pengalihan</td>
						</tr></th>";
			foreach ($barang->result() as $row) {
				$table .= "<tr>
							<td width='200'>".$row->id_barang."</td>
							<td width='300'>".$row->nama_barang."</td>
							<td width='200'>".$row->kondisi."</td>
							<td width='200'>".$row->tanggal_pembelian."</td>
							<td width='200'>".$this->barang->get_tr_pengalihan($row->id_barang)."
								<a href='".site_url("application/history_pengalihan/$row->id_barang")."'>View History</a>
							</td>
							<td width='200'>".$this->barang->get_tr_perbaikan($row->id_barang)."
								<a href='".site_url("application/history_perbaikan/$row->id_barang")."'>View History</a>
							</td>
						</tr>";
			}
			$table .= "</table><br/><br/>";
			
			
			
			$data = array(
				"web_title"=>"Laporan Barang",
				"page_title"=>"Laporan Barang",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"table",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$data['table']= $table;
			$output = $data;
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}
	/*public function history_barang(){
		try{
			$crud = new grocery_CRUD();
			$crud->set_theme('flexigrid');
			$crud->unset_operations();
			$crud->set_table('barang');
			$crud->set_subject('History Barang');
			$crud->set_relation('id_kategori','kategori_brg','kategori_brg');
			$crud->set_relation('id_ruang','ruangan','nama_ruang');
			//$crud->columns('id_barang','nama_barang','id_kategori','merk','spesifkasi','kondisi','id_ruang','tanggal_pembelian','harga');
			//$crud->display_as('id_kategori','Kategori Barang');
			//$crud->display_as('id_ruang','Lokasi');
			$crud->set_relation_n_n('Tanggal Pengalihan','tr_pengalihan','pengalihan','id_barang','id_pengalihan','tanggal');
			
			$output = $crud->render();
			$data = array(
				"web_title"=>"History Barang",
				"page_title"=>"History Barang",
				"navigasi"=>"menu_utama",
				"toolbar"=>"none",
				"page_name"=>"crud",
				"sidebar"=>"sidebar_utama",
				"dashboard"=>"active",
				"report"=>""
			);
			$output = array_merge($data,(array)$output);
			$this->output($output);
		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}*/
	public function history_pengalihan_barang(){
		echo 'test';
	}
}