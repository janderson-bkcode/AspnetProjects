 
try {

    const qrcode = require('qrcode-terminal');

    const { Client } = require('whatsapp-web.js');
    const client = new Client();

    client.on('qr', qr => {
        qrcode.generate(qr, {small: true});
    });

    client.on('ready', () => {
        console.log('Client is ready!');
    });

client.on('message', message => {

    contact = message.getContact();
    
    if (message.body.includes("oi")) {
        message.reply("Olá sou Janderson✅");
    }

    console.log(contact.number);
    
});
        
client.initialize();
    
} catch (error) {

    console.log(error);
}

// const qrcode = require('qrcode-terminal');

// const {
//     Client
// } = require('whatsapp-web.js');
// const client = new Client(
// );

// client.on('qr', qr => {
//     qrcode.generate(qr, {
//         small: true
//     });
// });

// client.on('ready', () => {
//     console.log('Client is ready!');
// });


// client.on('message', message => {
// 	if(message.body === '!ping') {
// 		client.sendMessage(message.from, 'pong');
// 	}
// });

// client.on('message', message => {

//     var contact = message.getContact();

//     if(!contact.IsMyContact){
//         if (message.body.includes("Janderson")) {
//             message.reply(message.body.replace("", "Janderson✅"));
//         }
//     }
   
// });


