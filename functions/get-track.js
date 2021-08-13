const { default: axios } = require("axios");

exports.handler = async function(event, context) {
    var track = await axios.get(
        `https://api.spotify.com/v1/audio-features/${event.queryStringParameters.id}`,
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
            body: JSON.stringify({track: track})
        }
    } catch (error) {
        return {
            statusCode: 500,
            body: error.toString()
        }
    }
}