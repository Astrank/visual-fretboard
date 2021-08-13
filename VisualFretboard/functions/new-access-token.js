const { default: axios } = require("axios");
const { access } = require("fs");
const qs = require('querystring');


exports.handler = async function(event, context) {
    const CLIENT_ID = process.env.CLIENT_ID;
    const CLIENT_SECRET = process.env.CLIENT_SECRET;
    
    try {
        var accessToken = await axios.post(
            "https://accounts.spotify.com/api/token",
            qs.stringify({
                grant_type: 'client_credentials',
            }),{
                headers: {
                "Authorization": "Basic " + Buffer.from(`${CLIENT_ID}:${CLIENT_SECRET}`).toString('base64'),
                'Content-Type': 'application/x-www-form-urlencoded'
                }
            }
        ).then(res => { return res.data })

        return {
            statusCode: 200,
            body: JSON.stringify({accessToken: accessToken})
        }
    } catch (error) {
        return {
            statusCode: 500,
            body: error.toString()
        }
    }
}