<script type="text/javascript">
	$(document).ready(function() {
		var oTable = $('#dataproduct').dataTable();
	} );
	function editRow ( oTable, nRow ){

	}
	function saveRow ( oTable, nRow ){

	}

	$('#dataproduct a.edit').live('click', function (e) {
		//alert('test');
		var itemIDValSplitter 	= (this.id).split("_");

		var row 				= itemIDValSplitter[1];
		var id 					= itemIDValSplitter[2];
		var specified			= parseInt($('#total').html())-parseInt($('#unspecified').html());
		var before 				= parseInt($("#stock_"+row).html());
		var maxval				= parseInt($("#unspecified").html())+parseInt($("#stock_"+row).html());

		$("#stock_"+row).before("<td id='stock_"+row+"'><input type='text' value='"+$('#stock_'+row).html()+"' id='input_"+row+"' /></td>").remove();
		$("#link_"+row+"_"+id).before("<a href='#' class='save' id='link_"+row+"_"+id+"'><img src='<?php echo base_url()."assets/admin/images/icon/save.png"; ?>' title='Save' witdh='15' height='15'/></a>").remove();

		$("#input_"+row).spinner();

		$("#input_"+row).bind('spinchange', function(event, ui) {
			var inc = 0;
			//var therest = specified - before;
			inc = before-this.value;


			$("#unspecified").before("<td id='unspecified'>"+(parseInt($("#unspecified").html())+inc)+"</td>").remove();

			if(parseInt($("#unspecified").html())<0){
				this.value = maxval;
				$("#unspecified").html(0);
			}
			if(this.value<0){
				this.value = 0;
				$("#unspecified").html(parseInt($("#total").html())-specified);
			}

			before = this.value;
			specified = parseInt($('#total').html())-parseInt($('#unspecified').html());

		});
	} );
	$('#dataproduct a.save').live('click', function (e) {
		//alert('test');
		var itemIDValSplitter 	= (this.id).split("_");

		var row 				= itemIDValSplitter[1];
		var id 					= itemIDValSplitter[2];

		$.ajax({
			type: "POST",
			url: "<?php echo base_url()?>admin/update_stock",
			data: { id_tochange: id, stock: $("#input_"+row).val(),tablename:$('#tablename').html()},
			success: function(data){
				$("#stock_"+row).before("<td id='stock_"+row+"'>"+$("#input_"+row).val()+"</td>").remove();
				$("#link_"+row+"_"+id).before("<a href='#' class='edit' id='link_"+row+"_"+id+"'><img src='<?php echo base_url()."assets/admin/images/icon/edit.png"; ?>' title='Edit' witdh='15' height='15'/></a>").remove();
			}
		});

	} );


</script>