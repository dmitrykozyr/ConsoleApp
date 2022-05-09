В GraphQL для всех запросов выбираем тип POST и URL https://localhost:7213/graphql

    #region GetList
    query {
	    a:list {
		    id
            name
            itemDatas {
                id
                title
                description
            }
	    }
        b: list {
            id
            name 
        }
    }
    #endregion

    #region Filtering
    query {
        list(where: { id: { eq: 3} })
        {
            id
            name
            itemDatas
            {
                title
                isDone
            }
        }
    }
    #endregion

    #region Sorting
    query {
        a: list(order: { name: ASC}) {
            id
            name
        }
        b: list(order: { name: DESC}) {
            id
            name
        }
    }
#endregion

    #region Mutation
    mutation {
        a: addList(input: { name: "Studying"}) {
            list {
                id
                name
            }
        }

        b: addItem(input: {
        title: "Prepare the oven"
            description: "Turn on the oven"
            isDone: false
            listId: 4
        })
        {
            data {
                id
                title
                description
            }
        }
    }
    #endregion
