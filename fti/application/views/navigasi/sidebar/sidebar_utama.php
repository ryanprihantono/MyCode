<ul class="accordion">
    <li id="title-leftbar">
        <p>Menu</p>
    </li>
    <?php
		$pages = $this->session->userdata('pages');
		/*$i=1;
		foreach($pages as $row){
			echo "<br>$row-";
			echo array_search($i,$pages);	
			$i++;
		}
		echo array_search("2",$pages)."qwer<br>";
		echo "<br>".array_search("15",$pages)."<br>";*/
	?>
    <li>
    	<a href="" class="accordion-title">Barang</a>
        <div class="accordion-content">
        	<ul>
            	<?php 
					if(array_search(1,$pages)!=NULL && array_search(1,$pages)!=""){ 
				?>
            	<li><a href="<?php echo site_url('application/kategori_barang'); ?>">Kategori Barang</a></li>
                <?php 
					}
					if(array_search(2,$pages)!=NULL && array_search(2,$pages)!=""){
				 ?>
                <li><a href="<?php echo site_url('application/barang'); ?>">Barang</a></li>
                <?php 
					}
					if(array_search(3,$pages)!=NULL && array_search(3,$pages)!=""){
				 ?>
                <li><a href="<?php echo site_url('application/pembelian'); ?>">Pembelian Barang</a></li>
                <?php 
					}
					if(array_search(4,$pages)!=NULL && array_search(4,$pages)!=""){
				 ?>
                <li><a href="<?php echo site_url('application/pengalihan'); ?>">Pengalihan Barang</a></li>
                <?php 
					}
					if(array_search(5,$pages)!=NULL && array_search(5,$pages)!=""){
				 ?>
                <li><a href="<?php echo site_url('application/perbaikan'); ?>">Perbaikan Barang</a></li>
                <?php 
					}
					if(array_search(6,$pages)!=NULL && array_search(6,$pages)!=""){
				 ?>
                <li><a href="<?php echo site_url('application/history_barang'); ?>">History Barang</a></li>
                <?php 
					}
					if(array_search(7,$pages)!=NULL && array_search(7,$pages)!=""){
				 ?>
                <li><a href="<?php echo site_url('application/view_laporan'); ?>">Laporan Barang</a></li>
                <?php 
					}
				 ?>
                
            </ul>
        </div>
    </li>
    <li>
    	<a href="" class="accordion-title">Ruangan</a>
        <div class="accordion-content">
        	<ul>
            	<?php 
					if(array_search(8,$pages)!=NULL && array_search(8,$pages)!=""){
				 ?>
                <li><a href="<?php echo site_url('application/kategori_ruangan'); ?>">Kategori Ruangan</a></li>
                <?php 
					}
					if(array_search(9,$pages)!=NULL && array_search(9,$pages)!=""){
				 ?>
                <li><a href="<?php echo site_url('application/ruangan'); ?>">Ruangan</a></li>
                <?php 
					}
				 ?>
            </ul>
        </div>
    </li>
    <li>
    	<a href="" class="accordion-title">Users Management</a>
        <div class="accordion-content">
        	<ul>
            	<?php 
					if(array_search(10,$pages)!=NULL && array_search(10,$pages)!=""){
				 ?>
                <li><a href="<?php echo site_url('application/roles'); ?>">Roles</a></li>
            	<?php 
					}
					if(array_search(11,$pages)!=NULL && array_search(11,$pages)!=""){
				 ?>
                <li><a href="<?php echo site_url('application/manage_user'); ?>">Manage Users</a></li>
                <?php 
					}
				 ?>
            </ul>
        </div>
    </li>
</ul>