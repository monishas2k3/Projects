/*=========================================
    DOM ELEMENTS
=========================================*/

const qrForm = document.getElementById("qr-form");
const qrText = document.getElementById("qr-text");

const qrSize = document.getElementById("qr-size");
const sizeValue = document.getElementById("size-value");

const qrColor = document.getElementById("qr-color");
const bgColor = document.getElementById("qr-bg-color");

const qrContainer = document.getElementById("qr-canvas-container");
const previewBox = document.getElementById("qr-preview-box");
const scanCard = document.getElementById("scan-card");

const downloadPNG = document.getElementById("download-png");
const downloadJPG = document.getElementById("download-jpg");

const copyBtn = document.getElementById("copy-btn");
const copyImageBtn = document.getElementById("copy-image-btn");
const shareBtn = document.getElementById("share-btn");


const errorText = document.getElementById("text-error");

const toast = document.getElementById("toast");

const themeToggle = document.getElementById("theme-toggle");

/*=========================================
    VARIABLES
=========================================*/

let qrCode = null;

const MAX_LENGTH = 500;

/*=========================================
    TOAST
=========================================*/

function showToast(message){

    toast.textContent = message;

    toast.classList.add("show");

    setTimeout(()=>{

        toast.classList.remove("show");

    },2500);

}

/*=========================================
    VALIDATION
=========================================*/

function validateInput(text){

    if(text.trim()===""){

        return "Please enter text or URL.";

    }

    if(text.length>MAX_LENGTH){

        return "Maximum 500 characters allowed.";

    }

    return "";

}

/*=========================================
    ERROR
=========================================*/

function showError(message){

    errorText.textContent = message;

}

function clearError(){

    errorText.textContent="";

}

/*=========================================
    SETTINGS
=========================================*/

function getSettings(){

    return{

        text:qrText.value.trim(),

        size:Number(qrSize.value),

        color:qrColor.value,

        bg:bgColor.value

    };

}

/*=========================================
    RESET
=========================================*/

function resetPreview(){

    qrContainer.innerHTML="";

    previewBox.classList.remove("has-result");
    
    scanCard.style.display="none";


    downloadBtn.disabled=true;

    copyBtn.disabled=true;

}

/*=========================================
    GENERATE QR
=========================================*/

function generateQRCode(){

    const settings=getSettings();

    const error=validateInput(settings.text);

    if(error){

        showError(error);

        resetPreview();

        return;

    }

    clearError();

    qrContainer.innerHTML="";

    qrCode=new QRCode(qrContainer,{

        text:settings.text,

        width:settings.size,

        height:settings.size,

        colorDark:settings.color,

        colorLight:settings.bg

    });

    previewBox.classList.add("has-result");

        scanCard.style.display="block";

        downloadPNG.disabled=false;
        downloadJPG.disabled=false;

        copyBtn.disabled=false;
        copyImageBtn.disabled=false;
        shareBtn.disabled=false;

}

/*=========================================
    LIVE UPDATE
=========================================*/

function liveUpdate(){

    if(qrText.value.trim()===""){

        resetPreview();

        clearError();

        return;

    }

    generateQRCode();

}

/*=========================================
    SIZE LABEL
=========================================*/

function updateSize(){

    sizeValue.textContent=

    qrSize.value+"px";

}

/*=========================================
        DOWNLOAD
=========================================*/

function getCanvas(){

    return qrContainer.querySelector("canvas");

}

downloadPNG.addEventListener("click",()=>{

    const canvas = getCanvas();

    if(!canvas) return;

    const link=document.createElement("a");

    link.download="QRCode.png";

    link.href=canvas.toDataURL("image/png");

    link.click();

    showToast("PNG Downloaded");

});

/*=========================================
        DOWNLOAD JPG
=========================================*/

downloadJPG.addEventListener("click",()=>{

    const canvas=getCanvas();

    if(!canvas) return;

    const jpgCanvas=document.createElement("canvas");

    jpgCanvas.width=canvas.width;

    jpgCanvas.height=canvas.height;

    const ctx=jpgCanvas.getContext("2d");

    ctx.fillStyle="#ffffff";

    ctx.fillRect(
        0,
        0,
        jpgCanvas.width,
        jpgCanvas.height
    );

    ctx.drawImage(canvas,0,0);

    const link=document.createElement("a");

    link.download="QRCode.jpg";

    link.href=jpgCanvas.toDataURL("image/jpeg",1);

    link.click();

    showToast("JPG Downloaded");

});

/*=========================================
        COPY TEXT
=========================================*/

copyBtn.addEventListener("click",()=>{

    navigator.clipboard.writeText(

        qrText.value

    );

    showToast("Text Copied");

});

/*=========================================
        COPY QR IMAGE
=========================================*/

copyImageBtn.addEventListener("click",async()=>{

    const canvas=getCanvas();

    if(!canvas){

        return;

    }

    canvas.toBlob(async(blob)=>{

        try{

            await navigator.clipboard.write([

                new ClipboardItem({

                    "image/png":blob

                })

            ]);

            showToast("QR Image Copied");

        }

        catch{

            showToast("Clipboard not supported");

        }

    });

});

/*=========================================
        SHARE QR
=========================================*/

shareBtn.addEventListener("click", async () => {

    const canvas = getCanvas();

    if (!canvas) return;

    if (navigator.share) {

        canvas.toBlob(async (blob) => {

            const file = new File(
                [blob],
                "QRCode.png",
                { type: "image/png" }
            );

            try {

                await navigator.share({

                    title: "QR Code",

                    text: qrText.value,

                    files: [file]

                });

            }

            catch {

                showToast("Share cancelled");

            }

        });

    }

    else{

        navigator.clipboard.writeText(qrText.value);

        showToast("Sharing not supported. Text copied.");

    }

});


/*=========================================
    DARK MODE
=========================================*/

function loadTheme() {

    const theme = localStorage.getItem("theme");

    if (theme === "dark") {

        document.body.classList.add("dark");

        themeToggle.innerHTML =
            '<i class="fa-solid fa-sun"></i>';

    }

}

themeToggle.addEventListener("click", () => {

    document.body.classList.toggle("dark");

    if (document.body.classList.contains("dark")) {

        localStorage.setItem("theme", "dark");

        themeToggle.innerHTML =
            '<i class="fa-solid fa-sun"></i>';

    }

    else {

        localStorage.setItem("theme", "light");

        themeToggle.innerHTML =
            '<i class="fa-solid fa-moon"></i>';

    }

});

/*=========================================
    EVENTS
=========================================*/

qrForm.addEventListener("submit", function (e) {

    e.preventDefault();

    generateQRCode();

});

qrText.addEventListener("input", liveUpdate);

qrSize.addEventListener("input", () => {

    updateSize();

    liveUpdate();

});

qrColor.addEventListener("input", liveUpdate);

bgColor.addEventListener("input", liveUpdate);

downloadBtn.addEventListener("click", downloadQRCode);

/*=========================================
    INITIALIZE
=========================================*/

updateSize();

loadTheme();

resetPreview();

/*=========================================
    OPTIONAL:
    Press Enter to Generate
=========================================*/

qrText.addEventListener("keypress", function (e) {

    if (e.key === "Enter") {

        e.preventDefault();

        generateQRCode();

    }

});

/*=========================================
    OPTIONAL:
    Save Last Text
=========================================*/

qrText.addEventListener("input", () => {

    localStorage.setItem(

        "lastQRText",

        qrText.value

    );

});

window.addEventListener("load", () => {

    const savedText =

        localStorage.getItem("lastQRText");

    if (savedText) {

        qrText.value = savedText;

    }

});

/*=========================================
    END OF FILE
=========================================*/