var current='';
var current_service='';
var current_cetak='';
var service_id='5';
var service_flag='';

function resetservice()
{
	 $('.rollover2').each(function(i) {
		service_id='5';
		service_flag='';
		$(this).attr('src','/asset/images/img/'+$(this).attr('title').replace('-on',''));
	});
}

$(function(){
	$('#btnCetak').click(function(){
		var ok=true;
		var nomorpolisi=$('#nomorpolisi').val();
		var tipe=$('#tipe').val();
		var merk=$('#merk').val()

		if(nomorpolisi=='')
		{
			alert('Nomor Polisi belum dimasukkan. Silahkan masukkan dahulu');
			$('#nomorpolisi').focus();
			ok=false;
		}
		else if(merk=='')
		{
			alert('Merk belum dimasukkan. Silahkan masukkan dahulu');
			$('#merk').focus();
			ok=false;
		}
		else if(tipe=='')
		{
			alert('Tipe belum dimasukkan. Silahkan masukkan dahulu');
			$('#tipe').focus();
			ok=false;
		}
		if(ok)
		{
			var base_url = document.getElementById("base_url").value;
			if(confirm('Apakah Anda yakin Nomor Polisi yang dimasukkan sudah benar?\nJika ya, Tekan OK.'))
			{
				var nopolVal = $('#nomorpolisi').val();
				var merkVal = $('#merk').val();
				var tipeVal = $('#tipe').val();
				$.ajax({ 
					url:base_url+'index.php/checkin/car_in',
					type: 'POST',
					dataType : "JSON",
					data : { nopol:nopolVal , merk: merkVal,tipe:tipeVal },
					success:function(data){					
						resetservice();
						$('#nomorpolisi').val('');
						$('#merk').val('');
						$('#tipe').val('');
						alert('Sedang Mencetak Kertas...\nTEKAN OK JIKA KERTAS SELESAI DICETAK.');
					},
					error:function(){
						alert('Kesalahan sistem');
					}
				});

			}
		}
	});

	$('#cetaknota').hover(
		function(){
			current_cetak_nota=$(this).attr('src');
			var base_url = document.getElementById("base_url").value;
			$(this).attr('src',base_url+'asset/images/img/'+$(this).attr('title'));
		},
		function()
		{
			$(this).attr('src',current_cetak_nota);
		}
	);

	$('#btnCetak').hover(
		function(){
			current_cetak=$(this).attr('src');
			var base_url = document.getElementById("base_url").value;
			$(this).attr('src',base_url+'asset/images/img/'+$(this).attr('title'));
		},
		function()
		{
			$(this).attr('src',current_cetak);
		}
	);

	$('.rollover2').click(function(){
		resetservice();
		whatclick=$(this).attr('id');
		service_id=whatclick.replace('service','');
		$(this).attr('src',base_url+'asset/images/img/'+$(this).attr('title'));
		service_flag=whatclick;
	});

	$('.rollover2').hover(
		function(){
			current_service=$(this).attr('src');
			$(this).attr('src',base_url+'asset/images/img/'+$(this).attr('title'));
		},
		function()
		{
			if(service_flag!=$(this).attr('id'))
			{
				$(this).attr('src',current_service);
			}
		}
	);
});