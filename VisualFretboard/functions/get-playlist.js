const { default: axios } = require("axios");

exports.handler = async function(event, context) {
    var playlist = await axios.get(
        `https://api.spotify.com/v1/playlists/${event.queryStringParameters.playlist}`,
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
            body: JSON.stringify({playlist: playlist})
        }
    } catch (error) {
        return {
            statusCode: 500,
            body: error.toString()
        }
    }
}