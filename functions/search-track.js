const { default: axios } = require("axios");

exports.handler = async function(event, context) {
    var tracks = await axios.get(
        `https://api.spotify.com/v1/search?q=${event.queryStringParameters.input}&type=track&limit=50`,
        {
            headers: {
                "Authorization": "Bearer " + event.queryStringParameters.token,
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        }).then(res => res.data);
        
    try {
        return {
            statusCode: 200,
            body: JSON.stringify({tracks: tracks})
        }
    } catch (error) {
        return {
            statusCode: 500,
            body: error.toString()
        }
    }
}