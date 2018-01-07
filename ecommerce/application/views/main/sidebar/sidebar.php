<?php
	foreach ($sidebar as $row){
		if($row=='login'){
			if($this->session->userdata('username')!=FALSE){
				echo "<div class='sidebar-box round5-top'>
    					<h1 class='sidebar-head'>My Profile</h1>
 					    <div class='sidebar-content'>
							<div id='sidebar-login'>Welcome,".$this->session->userdata('username')."<br><br><a href='".base_url()."main/dologout"."' style='color:#000'><button type='submit' class='uniform'>Logout</button></a>
							</div>
							<div style='margin-top:5px'>
								<a href='".base_url()."main/profile' style='text-decoration:none;color:black'>My Profile <img src='".base_url()."assets/admin/images/icon/setting.png' title='Edit Profile' /></a>
							</div>
						</div>
					</div>";
			}
			else{
				include "$row.php";
			}
		}
		else{
			include "$row.php";
		}
	}
?>