import { Vrtic } from "./Vrtic.js";
import { Administrator } from "./Administrator.js";

var listaVrtica=[];

function ucitajVrtice(){


    fetch("https://localhost:5001/Vrtic/PreuzmiVrtic")
    .then(p=>{
        p.json().then(vrtici=>{ 
            vrtici.forEach(vrtic => {
                var v=new  Vrtic(vrtic.id, vrtic.naziv);
                listaVrtica.push(v);
            });
            crtajPocetnu(document.body);
            })
    })
}
ucitajVrtice();

var listaDece=[];

function ucitajDecu()
{
let vrednostVrtic=document.querySelector(".vrtic");
var vrticId=vrednostVrtic.options[vrednostVrtic.selectedIndex].value;
//console.log(vrticId);
var aktivnostId= document.querySelector("input[type='radio']:checked").value;

    fetch("https://localhost:5001/Dete/PrikaziDecu/"+vrticId+"/"+aktivnostId,
    {
        method:"GET"
    }).then(s=>{
        if(s.ok)
        {
            listaDece.length=0;
            s.json().then(data=>{
                data.forEach(el=>{
                    //console.log(el);
                    listaDece.push(el);
                    
                });
                //console.log(listaDece);
                PrikaziDecu();
        })
    }
    })

}
function PrikaziDecu()
{
    let divPrikazTabele=document.querySelector(".divPrikazTabele");
    obrisiFormu(divPrikazTabele);
    var divUnos=document.createElement("div");
        divUnos.className="divUnos";
        divPrikazTabele.appendChild(divUnos);
        var divTabela=document.createElement("div");
        divTabela.className="divTabela";
        divPrikazTabele.appendChild(divTabela);
    unosDete();
    let label=document.createElement("label");
    label.innerHTML="Deca";
    
    let tr=document.querySelector(".red");
    let tabelaBody=document.querySelector(".bodyDeo");
    let th;
    var zag=["Ime","Prezime","Broj roditelja"];
    zag.forEach(e=>{
        th=document.createElement("th");
        th.innerHTML=e;
        tr.appendChild(th);
    })
    let td;
    listaDece.forEach(el=>{

        tr=document.createElement("tr");
        tabelaBody.appendChild(tr);

        tr.id=el.id;
       
        tr.addEventListener("dblclick", () => {
            tr.className = "neselektovan";
            let btn=document.querySelector(".UpisDete");
            btn.disabled=false; 
            let Tekst = document.querySelector(".txbIme");
            Tekst.value = "";
            Tekst = document.querySelector(".txbPrezime");
            Tekst.value = "";
            Tekst = document.querySelector(".txbBroj");
            Tekst.value = "";
            Tekst = document.querySelector(".txbJMBG");
            Tekst.placeholder="";

        });

        tr.addEventListener("click", () => {
        tr.className = "selektovan";
        let btn=document.querySelector(".UpisDete");
        btn.disabled=true; 
        document.querySelector(".txbIme").disabled=true; 
        document.querySelector(".txbPrezime").disabled=true;

        let Tekst = document.querySelector(".txbIme");
        Tekst.value = el.ime;
        Tekst = document.querySelector(".txbPrezime");
        Tekst.value = el.prezime;
        Tekst = document.querySelector(".txbBroj");
        Tekst.value = el.brojRoditelja;
        Tekst = document.querySelector(".txbJMBG");
        Tekst.placeholder="Unesite JMBG deteta";
        
        }); 
        

        td=document.createElement("td");
        td.innerHTML=el.ime;
        tr.appendChild(td);

        td=document.createElement("td");
        td.innerHTML=el.prezime;
        tr.appendChild(td);


        td=document.createElement("td");
        td.innerHTML=el.brojRoditelja;
        tr.appendChild(td);

        
    })
   
    let divStatistika=document.createElement("div");
    divStatistika.className="divStatistika";
    document.querySelector(".pomocnaForma").appendChild(divStatistika);
    
    let divGr=document.createElement("div");
    divGr.className="divGr";
    divStatistika.appendChild(divGr);

    
    prikaziDeteVrtic();
}
function unosDete()
{
    let divUnos=document.querySelector(".divUnos");

    let lblN=document.createElement("label");
    lblN.innerHTML="Unesite podatke o korisniku:"
    lblN.className="labVasp";
    divUnos.appendChild(lblN);

    let divLabele=document.createElement("div");
    divLabele.className="divLabele";
    divUnos.appendChild(divLabele);



    let labelica1=document.createElement("label");
    labelica1.innerHTML="Ime:";
    divLabele.appendChild(labelica1);


    let txbIme=document.createElement("input");
    txbIme.type="text";
    txbIme.className="txbIme";

    divLabele.appendChild(txbIme);

    let labelica2=document.createElement("label");
    labelica2.innerHTML="Prezime:";
    divLabele.appendChild(labelica2);

    let txbPrezime=document.createElement("input");
    txbPrezime.type="text";
    txbPrezime.className="txbPrezime";

    divLabele.appendChild(txbPrezime);


    let labelica=document.createElement("label");
    labelica.innerHTML="Broj roditelja:";
    divLabele.appendChild(labelica);

    let txbBroj=document.createElement("input");
    txbBroj.type="number";
    txbBroj.className="txbBroj";

    divLabele.appendChild(txbBroj);

   let labelica3=document.createElement("label");
    labelica3.innerHTML="JMBG:";
    divLabele.appendChild(labelica3);

    let txbJMBG=document.createElement("input");
    txbJMBG.type="number";
    txbJMBG.className="txbJMBG";

    divLabele.appendChild(txbJMBG);

    let divPrvi=document.createElement("div");
    divPrvi.className="divPrvi";
    divUnos.appendChild(divPrvi);


    let btnUpisi=document.createElement("button");
    btnUpisi.innerHTML="Upisi dete";
    btnUpisi.className="UpisDete";

 
    divPrvi.appendChild(btnUpisi);

    let btnObrisi=document.createElement("button");
    btnObrisi.innerHTML="Ispisi dete";
    btnObrisi.className="IspisDete";
    divPrvi.appendChild(btnObrisi);


    let btnIzmeni=document.createElement("button");
    btnIzmeni.innerHTML="Izmeni dete";
    btnIzmeni.className="IzmeniDete";
    divPrvi.appendChild(btnIzmeni);

    kreirajTabelu();

    btnUpisi.onclick=(e)=>dodajDete();
    btnObrisi.onclick=(e)=>vratiDeteBrisanje();
    btnIzmeni.onclick=(e)=>izmeniDete();
}
var deteId;
var listaDeceVrtic=[];
function prikaziDeteVrtic()
{
    let vrednostVrtic=document.querySelector(".vrtic");
    var vrticId=vrednostVrtic.options[vrednostVrtic.selectedIndex].value;
    fetch("https://localhost:5001/Dete/PrikaziDecu/"+vrticId,
    ).then(p=>{
        if(p.ok)
        {listaDeceVrtic.length=0;
        p.json().then(deca=>{
            deca.forEach(el=>
                {
                    listaDeceVrtic.push(el);
                });
                kreirajStatistiku();
        })
        }
    }
    )
}
function vratiDete()
{
let vrednostVrtic=document.querySelector(".vrtic");
var vrticId=vrednostVrtic.options[vrednostVrtic.selectedIndex].value;
var jmbg=document.querySelector(".txbJMBG").value;
fetch("https://localhost:5001/Dete/VratiDete/"+vrticId+"/"+jmbg,
{
    method:"GET"
}).then(s=>
    {
        if(s.ok)
        {
            s.json().then(data=>{
                    deteId=data.id;
                    //console.log(deteId);
                    dodajDetePom();
                     })
            
    }
        
    }
    
    
)

}
function vratiDeteBrisanje()
{
let vrednostVrtic=document.querySelector(".vrtic");
var vrticId=vrednostVrtic.options[vrednostVrtic.selectedIndex].value;
var jmbg=document.querySelector(".txbJMBG").value;
fetch("https://localhost:5001/Dete/VratiDete/"+vrticId+"/"+jmbg,
{
    method:"GET"
}).then(s=>
    {
        if(s.ok)
        {
            s.json().then(data=>{
                    deteId=data.id;
                    console.log(deteId);
                    obrisiDete();
                     })
            
    }
    else
    {
        alert("Nevalidan jmbg deteta");
    }
        
    }
    
    
)
}
function dodajDete()
{
let vrednostVrtic=document.querySelector(".vrtic");
var vrticId=vrednostVrtic.options[vrednostVrtic.selectedIndex].value;
var ime=document.querySelector(".txbIme").value;
var prezime=document.querySelector(".txbPrezime").value;
var jmbg=document.querySelector(".txbJMBG").value;
var brojRoditelja=document.querySelector(".txbBroj").value;
//vratiDete();
console.log(jmbg);
console.log(ime);
console.log(prezime);
console.log(brojRoditelja);

if(ime=="")
{
    alert("Morate da unesete ime");
}
if(prezime=="")
{
    alert("Morate da unesete prezime");
}
if(brojRoditelja=="")
{
    alert("Morate da unesete broj roditelja");
}
if(jmbg=="" || jmbg.length!=13)
{
    alert("Morate da unesete jmbg deteta");
}

fetch("https://localhost:5001/Dete/DodatiDete/"+ime+"/"+prezime+"/"+brojRoditelja+"/"+jmbg+"/"+vrticId,
{
    method:"POST"
}).then(s=>
    {
        if(s.ok)
        {   
            vratiDete();
        }
      
       
    }
    
    
)

}
function dodajDetePom()
{
    //console.log(deteId);
   var aktivnostId= document.querySelector("input[type='radio']:checked").value;
            fetch("https://localhost:5001/Ucestvuje/DeteUcestvuje/"+deteId+"/"+aktivnostId,
            {
                method:"POST"
            }).then(s=>
                {
                    if(s.ok)
                    {
                        ucitajDecu();
                       // return ("Uspesno ste dodali dete");
                    }
                })
  
}
function obrisiDetePom()
{
    var ime=document.querySelector(".txbIme").value;
var prezime=document.querySelector(".txbPrezime").value;
var jmbg=document.querySelector(".txbJMBG").value;
var brojRoditelja=document.querySelector(".txbBroj").value;
    fetch("https://localhost:5001/Dete/ObrisiDete/"+ime+"/"+prezime+"/"+brojRoditelja+"/"+jmbg,
    {
        method:"DELETE"
    })
    .then(p=>{
       if(p.ok)
       {
           ucitajDecu();
       }
       else
       {
           alert("Problem pri brisanju");
       }
    })
}
function obrisiDete()
{
    var aktivnostId= document.querySelector("input[type='radio']:checked").value;
    fetch("https://localhost:5001/Ucestvuje/DeteUcestvujeIzbrisi/"+deteId+"/"+aktivnostId,
    {
        method:"DELETE"
    })
    .then(p=>{
       if(p.ok)
       {
           console.log("Uspesno ste obrisali dete");
           obrisiDetePom();
       }
       else
       {
           alert("Problem pri brisanju");
       }
    })
}
function izmeniDete()
{
    var ime=document.querySelector(".txbIme").value;
    var prezime=document.querySelector(".txbPrezime").value;
    var jmbg=document.querySelector(".txbJMBG").value;
    var brojRoditelja=document.querySelector(".txbBroj").value;
    fetch("https://localhost:5001/Dete/IzmeniDete/"+ime+"/"+prezime+"/"+brojRoditelja+"/"+jmbg,
    {
        method:"PUT"
    }).then(s=>{
        if(s.ok)
        {
                ucitajDecu();
        }
         else
        {
        alert("Jmbg se ne poklapa sa vasim detetom");
        }
    })
}

var listaAktivnosti=[];
function ucitajAktivnosti(host,tmp)
{
let vrednostVrtic=document.querySelector(".vrtic");
var vrticId=vrednostVrtic.options[vrednostVrtic.selectedIndex].value;
//console.log(vrticId);
fetch("https://localhost:5001/Aktivnost/PreuzmiAktivnost/"+vrticId,
{
    method:"GET"
}).then(s=>
    {
        if(s.ok)
        {
            listaAktivnosti.length=0;
            s.json().then(data=>{
                data.forEach(el=>{
                    listaAktivnosti.push(el);
                    console.log(el);
                });
                prikaziAktivnosti(host,tmp);
            })
            
        }
        
    }
    
    
)

}
function prikaziAktivnosti(host,tmp)
{
   // obrisiFormu(host);
    let radio=document.createElement("div");
        radio.className="radioPrikaz";
        host.appendChild(radio);
    //console.log(radio);
    let rb;
    let lb;
    let div;
    let pomocnaForma=document.querySelector(".pomocnaForma");
    let divPrikazTabele=document.createElement("div");
    divPrikazTabele.className="divPrikazTabele";

    pomocnaForma.appendChild(divPrikazTabele);
    listaAktivnosti.forEach(el=>
        {
            div=document.createElement("div");
            radio.appendChild(div);
           rb=document.createElement("input");
           rb.type="radio";
           rb.checked="true";
           rb.name="izaberi";
           rb.value=el.id;
           div.appendChild(rb);

           lb=document.createElement("label");
           lb.innerHTML=el.naziv;
           div.appendChild(lb);
           if(tmp==1)
           {
           rb.onchange=(e)=>ucitajDecu();
           }
           else
           {
            rb.onchange=(e)=>ucitajVaspitace(); 
           }
        })
    if(tmp==1)
    {
    ucitajDecu();
    }
    else
    {
        ucitajVaspitace();
    }
    

}
function kreirajStatistiku() {
    let divPrikazTabele= document.querySelector(".divGr");
        obrisiFormu(divPrikazTabele);
    listaAktivnosti.forEach(i => {
        let aktivnostDiv = document.createElement("div");
        aktivnostDiv.className = "asktivnostDiv";
        divPrikazTabele.appendChild(aktivnostDiv);
        let divTxt = document.createElement("div");
        divTxt.className = "divTxt";
        aktivnostDiv.appendChild(divTxt);
        let divGrafik = document.createElement("div");
        divGrafik.className = "divGrafik";
        aktivnostDiv.appendChild(divGrafik);
        let brojac = 0;
        let unutrasnjiDiv;
        console.log(listaDeceVrtic);
        listaDeceVrtic.forEach(j => {
            unutrasnjiDiv = document.createElement("div");
            unutrasnjiDiv.className = "unutrasnjiDiv";
            if (j.idAktivnosti == i.id) {
                divGrafik.appendChild(unutrasnjiDiv);
                brojac++;
            }
        })
        let txtNaziv = document.createTextNode(i.naziv);
        let br = document.createElement("br");
        let txtKol = document.createTextNode("Broj dece:" + brojac);
        divTxt.appendChild(txtNaziv);
        divTxt.appendChild(br);
        divTxt.appendChild(txtKol);
    })
}

function crtajPocetnu(host)
{
    let glavna=document.createElement("div");
    glavna.className="glavnaForma";
    host.appendChild(glavna);

    let labelaDiv=document.createElement("div");
    labelaDiv.className="labelaDiv";
    //glavna.appendChild();

    let l=document.createElement("label");
    l.innerHTML="IZABERITE VRTIC:";
    l.className="naslov";
    labelaDiv.appendChild(l);


    let vrticForma=document.createElement("div");
    vrticForma.className="vrticForma";
    glavna.appendChild(vrticForma);

    vrticForma.appendChild(labelaDiv);

    let divSelect=document.createElement("div");
    divSelect.className="divSelect";
    vrticForma.appendChild(divSelect);

        let se=document.createElement("select");
        se.className="vrtic";
        divSelect.appendChild(se);

        let op;
        listaVrtica.forEach(p => {
            op=document.createElement("option");
            op.innerHTML=p.naziv;
            op.value=p.id;
            se.appendChild(op);  
        });

    let divDugmici=document.createElement("div");
    divDugmici.className="divDugmici";
    glavna.appendChild(divDugmici);

    let btnKorisnik=document.createElement("button");
    btnKorisnik.innerHTML="Korisnik";
    btnKorisnik.className="Korisnik";
    divDugmici.appendChild(btnKorisnik);

    let btnAdministrator=document.createElement("button");
    btnAdministrator.innerHTML="Administrator";
    btnAdministrator.className="Administrator";
    divDugmici.appendChild(btnAdministrator);

    let pomocnaForma=document.createElement("div");
    pomocnaForma.className="pomocnaForma";
    glavna.appendChild(pomocnaForma);


    btnKorisnik.onclick=(e)=>crtajKorisnik(pomocnaForma);
    btnAdministrator.onclick=(e)=>crtajAdministrator(pomocnaForma);

}

function crtajKorisnik(host)
    {
        obrisiFormu(host);

        let lab=document.createElement("label");
        lab.innerHTML="Izaberite aktivnost:";
        lab.className="lblAktivnosti";
        host.appendChild(lab);

        let kontPomocna=document.createElement("div");
        kontPomocna.className="radioPrikaz";
        host.appendChild(kontPomocna);

        let s=document.querySelector(".vrtic");
        s.disabled=false;
        ucitajAktivnosti(kontPomocna,1);
        document.querySelector(".vrtic").onchange=(e)=>crtajKorisnik(host);
}

function obrisiFormu(forma) 
{
    while (forma.firstChild)
    {
        console.log(forma.firstChild);
        forma.removeChild(forma.firstChild);
        
    }
}
function kreirajTabelu()
    {
        var divTabela=document.querySelector(".divTabela");
        var tabela=document.createElement("table");
        tabela.className="tabela";
        divTabela.appendChild(tabela);
    
        var tabelaHead=document.createElement("thead");
        tabela.appendChild(tabelaHead);
        
        var tr=document.createElement("tr");
        tr.className="red";
        tabelaHead.appendChild(tr);
    
        var tabelaBody=document.createElement("tbody");
        tabelaBody.className="bodyDeo";
        tabela.appendChild(tabelaBody);
}
function crtajAdministrator(host)
    {
        obrisiFormu(host);
        

        let divAdministratorF=document.createElement("div");
        divAdministratorF.className="divAdministratorF";
        host.appendChild(divAdministratorF);

        

        let divAdministrator=document.createElement("div");
        divAdministrator.className="divAdministrator";
        divAdministratorF.appendChild(divAdministrator);

        
        let naslov=document.createElement("label");
        naslov.innerHTML="PRIJAVITE SE:"; 
        naslov.className="naslov";
        divAdministrator.appendChild(naslov);
    
        let lblIme=document.createElement("label");
        lblIme.innerHTML="Ime"; 
        divAdministrator.appendChild(lblIme);
    
        let ime=document.createElement("input");
        ime.type="text";
        ime.className="ImeA";
        divAdministrator.appendChild(ime);
    
        let lblJMBG=document.createElement("label");
        lblJMBG.innerHTML="JMBG";
        divAdministrator.appendChild(lblJMBG);
    
        let JMBG=document.createElement("input");
        JMBG.type="number";
        JMBG.className="jmbgA";
        divAdministrator.appendChild(JMBG);
    
        let lblSifra=document.createElement("label");
        lblSifra.innerHTML="Sifra";
        divAdministrator.appendChild(lblSifra);
    
        let sifra=document.createElement("input");
        sifra.type="password";
        sifra.className="SIFRA";
        divAdministrator.appendChild(sifra);
    
        let btnPotvrdi=document.createElement("button");
        btnPotvrdi.innerHTML="Potvrdi";
        btnPotvrdi.className="Potvrdi";
        divAdministrator.appendChild(btnPotvrdi);

        btnPotvrdi.onclick=(e)=>vratiAdministratora();
}

function vratiAdministratora(){
    let vrednostVrtic=document.querySelector(".vrtic");
    var vrticId=vrednostVrtic.options[vrednostVrtic.selectedIndex].value;
    fetch("https://localhost:5001/Administrator/PreuzmiAdministratora/"+vrticId,
    {
        method:"GET"
    }).then(s=>{
            s.json().then(data=>{
                var ad=new Administrator(data.id,data.ime,data.jmbg,data.sifra);
                console.log(ad);
              potvrdiAdministratora(ad);
                })
    })
}

function potvrdiAdministratora(adm)
{
   // console.log(adm);
    var i=document.querySelector(".ImeA");
    console.log(i.value);
    if(adm.ime===i.value)
    {
        var jmbg=document.querySelector(".jmbgA");
        if(adm.jmbg==jmbg.value)
        {
            
            var sifra=document.querySelector(".SIFRA");
            if(adm.sifra==sifra.value)
            {
                otvoriFormuAdministrator(document.querySelector(".pomocnaForma"));
            }
            else
            {
                alert("Pogresna sifra");
            }
        }
        else
        {
            alert("Pogresan jmbg");
        }
    }
    else{
        alert("Pogresno ime");
    }
    

   
}
function otvoriFormuAdministrator(host)
{
    obrisiFormu(host);

    let lab=document.createElement("label");
        lab.innerHTML="Izaberite aktivnost:";
        lab.className="lblAktivnosti";
        host.appendChild(lab);

    let kontPomocna=document.createElement("div");
    kontPomocna.className="radioPrikaz";
    host.appendChild(kontPomocna);

    let s=document.querySelector(".vrtic");
    s.disabled=true;

    ucitajAktivnosti(kontPomocna,0);


  
}


var listaVaspitaca=[];
function ucitajVaspitace()
{
    let vrednostVrtic=document.querySelector(".vrtic");
    var vrticId=vrednostVrtic.options[vrednostVrtic.selectedIndex].value;
    console.log(vrticId);
    var aktivnostId= document.querySelector("input[type='radio']:checked").value;

    fetch("https://localhost:5001/Vaspitac/PrikaziVaspitace/"+vrticId+"/"+aktivnostId,
    {
        method:"GET"
    }).then(s=>{
        if(s.ok)
        {
            listaVaspitaca.length=0;
            s.json().then(data=>{
                data.forEach(el=>{
                    console.log(el.id)
                    listaVaspitaca.push(el);
                });
               PrikaziVaspitace();
        })
    }
    })
}
var vaspitacId;
function PrikaziVaspitace()
{
    let divPrikazTabele=document.querySelector(".divPrikazTabele");
    obrisiFormu(divPrikazTabele);
    var divUnos=document.createElement("div");
        divUnos.className="divUnos";
        divPrikazTabele.appendChild(divUnos);
        var divTabela=document.createElement("div");
        divTabela.className="divTabela";
        divPrikazTabele.appendChild(divTabela);
    unosVaspitac();
    let label=document.createElement("label");
    label.innerHTML="Vaspitaci";
    
    let tr=document.querySelector(".red");
    let tabelaBody=document.querySelector(".bodyDeo");
    let th;
    var zag=["Ime","Prezime","JMBG"];
    zag.forEach(e=>{
        th=document.createElement("th");
        th.innerHTML=e;
        tr.appendChild(th);
    })
    let td;
    listaVaspitaca.forEach(el=>{

        tr=document.createElement("tr");
        tabelaBody.appendChild(tr);

        tr.id=el.id;
        vaspitacId=el.id;
        tr.addEventListener("dblclick", () => {
            tr.className = "neselektovan";
            let btn=document.querySelector(".Zaposli");
            btn.disabled=false; 
           // btn=document.querySelector(".Premesti");
           // btn.disabled=false; 

            let Tekst = document.querySelector(".txbImeV");
            Tekst.value = "";
            Tekst = document.querySelector(".txbPrezimeV");
            Tekst.value = "";
            Tekst = document.querySelector(".txbJMBGV");
            Tekst.value="";

        });

        tr.addEventListener("click", () => {
        tr.className = "selektovan";
        let btn=document.querySelector(".Zaposli");
        btn.disabled=true; 
        //btn=document.querySelector(".Premesti");
        //btn.disabled=true;

        let Tekst = document.querySelector(".txbImeV");
        Tekst.value = el.ime;
        Tekst = document.querySelector(".txbPrezimeV");
        Tekst.value = el.prezime;
        Tekst = document.querySelector(".txbJMBGV");
        Tekst.value=el.jmbg;
        
        }); 
        

        td=document.createElement("td");
        td.innerHTML=el.ime;
        tr.appendChild(td);

        td=document.createElement("td");
        td.innerHTML=el.prezime;
        tr.appendChild(td);


        td=document.createElement("td");
        td.innerHTML=el.jmbg;
        tr.appendChild(td);
    })
        
}
function unosVaspitac()
{
    let divUnos=document.querySelector(".divUnos");

    let lblN=document.createElement("label");
    lblN.innerHTML="Unesite podatke o vaspitacu:"
    lblN.className="labVasp";
    divUnos.appendChild(lblN);

    let divLabele=document.createElement("div");
    divLabele.className="divLabele";
    divUnos.appendChild(divLabele);



    let labelica1=document.createElement("label");
    labelica1.innerHTML="Ime:";
    divLabele.appendChild(labelica1);


    let txbIme=document.createElement("input");
    txbIme.type="text";
    txbIme.className="txbImeV";

    divLabele.appendChild(txbIme);

    let labelica2=document.createElement("label");
    labelica2.innerHTML="Prezime:";
    divLabele.appendChild(labelica2);

    let txbPrezime=document.createElement("input");
    txbPrezime.type="text";
    txbPrezime.className="txbPrezimeV";

    divLabele.appendChild(txbPrezime);


   let labelica3=document.createElement("label");
    labelica3.innerHTML="JMBG:";
    divLabele.appendChild(labelica3);

    let txbJMBG=document.createElement("input");
    txbJMBG.type="number";
    txbJMBG.className="txbJMBGV";

    divLabele.appendChild(txbJMBG);

    let divPrvi=document.createElement("div");
    divPrvi.className="divPrvi";
    divUnos.appendChild(divPrvi);


    let btnUpisi=document.createElement("button");
    btnUpisi.innerHTML="Zaposli";
    btnUpisi.className="Zaposli";

  
    divPrvi.appendChild(btnUpisi);

    let btnObrisi=document.createElement("button");
    btnObrisi.innerHTML="Otpusti";
    btnObrisi.className="Otkaz";
    divPrvi.appendChild(btnObrisi);

    let btnPremesti=document.createElement("button");
    btnPremesti.innerHTML="Premesti";
    btnPremesti.className="Premesti";
    divPrvi.appendChild(btnPremesti);

    let se=document.createElement("select");
        se.className="selectAktivnost";
        divPrvi.appendChild(se);

        let op;
        var aktivnostId= document.querySelector("input[type='radio']:checked").value;
        listaAktivnosti.forEach(p => {
            if(p.id!=aktivnostId)
            {
            op=document.createElement("option");
            op.innerHTML=p.naziv;
            op.value=p.id;
            se.appendChild(op); 
            } 
        });
        

    kreirajTabelu();

    btnUpisi.onclick=(e)=>dodajVaspitaca();
    btnObrisi.onclick=(e)=>obrisiVaspitaca();
   btnPremesti.onclick=(e)=>premestiVaspitaca();
}
function dodajVaspitaca()
{
    let vrednostVrtic=document.querySelector(".vrtic");
    var vrticId=vrednostVrtic.options[vrednostVrtic.selectedIndex].value;
    var ime=document.querySelector(".txbImeV").value;
    var prezime=document.querySelector(".txbPrezimeV").value;
    var jmbg=document.querySelector(".txbJMBGV").value;
    console.log(ime);
    console.log(prezime);
    console.log(jmbg);
    
    fetch("https://localhost:5001/Vaspitac/DodajVaspitaca/"+ime+"/"+prezime+"/"+jmbg+"/"+vrticId,
    {
        method:"POST"
   }).then(s=>
        {
            if(s.ok)
            {   
               dodajVaspitacNadgleda();
            }
            else
            {
                alert("Pogresni podatak za vaspitaca");
            }
           
        }
        
        
    )
    
}
function dodajVaspitacNadgleda()
{
    var aktivnostId= document.querySelector("input[type='radio']:checked").value;
    var jmbgVaspitaca=document.querySelector(".txbJMBGV").value;
    fetch("https://localhost:5001/Nadgleda/NadgledaAktivnost/"+jmbgVaspitaca+"/"+aktivnostId,
{
    method:"POST"
}).then(s=>
    {
        if(s.ok)
        {
            ucitajVaspitace();
            //console.log("Uspenso ste dodali vaspitacu aktivnost za nadgledanje");
        }
        
    }
    
    
)
}
function obrisiVaspitaca()
{
    var aktivnostId= document.querySelector("input[type='radio']:checked").value;
    fetch("https://localhost:5001/Nadgleda/VaspitacNadgledaIzbrisi/"+vaspitacId+"/"+aktivnostId,
    {
        method:"DELETE"
    })
    .then(p=>{
       if(p.ok)
       {
           console.log("Uspesno ste obrisali dete");
           obrisiVaspitacaPom();
       }
       else
       {
           alert("Prblem pri brisanju");
       }
    })
}

function obrisiVaspitacaPom()
{
    var jmbg=document.querySelector(".txbJMBGV").value;
        fetch("https://localhost:5001/Vaspitac/ObrisiVaspitaca/"+jmbg,
        {
            method:"DELETE"
        })
        .then(p=>{
           if(p.ok)
           {
               ucitajVaspitace();
           }
           else
           {
               alert("Prblem pri brisanju");
           }
        })
}
function premestiVaspitaca()
{
    var aktivnostId= document.querySelector("input[type='radio']:checked").value;
    let vrednostAktivnost=document.querySelector(".selectAktivnost");
    var novaAktivnostId=vrednostAktivnost.options[vrednostAktivnost.selectedIndex].value;
    fetch("https://localhost:5001/Nadgleda/IzmeniVaspitacNadgleda/"+vaspitacId+"/"+aktivnostId+"/"+novaAktivnostId,
    {
        method:"PUT"
    }).then(s=>{
        if(s.ok)
        {
                ucitajVaspitace();
    }
    })


}


