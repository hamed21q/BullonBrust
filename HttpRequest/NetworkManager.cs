using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private GetRequest getRequest;
    private PostRequest postRequest;
    public string leaderBoardUrl;
    public string registerUrl;
    public string loginUrl;
    public string uploadScoreUrl;
    private void Awake()
    {
        postRequest = new PostRequest(new JsonSerialization());
        getRequest = new GetRequest(new JsonSerialization());
    }
    public async Task<LeaderBoardList> GetLeaderBoardData()
    {
        var data = await getRequest.Send<LeaderBoardList>(leaderBoardUrl);
        return data;
    }
    public async Task<Response> Register(WWWForm form)
    {
        Response response = await postRequest.Send(registerUrl, form);
        return response;
    }
    public async Task<Response> Login(WWWForm form)
    {
        Response response = await postRequest.Send(loginUrl, form);
        return response;
    }
    public async Task<Response> UpdateHighScore(WWWForm form, string token)
    {
        Response response = await postRequest.Send(uploadScoreUrl, form, token);
        return response;
    }
}