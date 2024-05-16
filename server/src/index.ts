import { AppDataSource } from "./data-source"
import { User } from "./entity/User"
import express, { Request } from "express";
import jwt from 'jsonwebtoken';
import bcrypt from 'bcrypt';
import bodyParser from 'body-parser';
import { Test } from "./entity/Test";
import { TestStats } from "./entity/TestStats";
import { Task } from "./entity/Task";
import { Theory } from "./entity/Theory";

interface LoginData {
    username: string;
    password: string;
}
interface Answer {
    id: string;
    text: string;
}

interface AnswerData {
    answers: Answer[];
}

interface ITask {
    text: string;
    answer: string;
}

interface TestData {
    name: string;
    course: 'algebra' | 'geometry' | 'statistics';
    tasks: ITask[];
}

interface CreateTheory {
    text: string;
    name: string;
    course: 'algebra' | 'geometry' | 'statistics';
}

interface CreateTask {
    text: string;
    answer: string;
}

interface CreateTest {
    name: string;
    course: 'algebra' | 'geometry' | 'statistics';
    tasks: CreateTask[];
}


const verify = (header: string): string => {
    // @ts-ignore
    return (jwt.verify(header.split(' ')[1], process.env.JWT_KEY)).id;
}

AppDataSource.initialize().then(async () => {
    const userRepo = AppDataSource.getRepository(User);
    const testRepo = AppDataSource.getRepository(Test);
    const statsRepo = AppDataSource.getRepository(TestStats);
    const theoryRepo = AppDataSource.getRepository(Theory);
    const taskRepo = AppDataSource.getRepository(Task);
    const app = express();
    app.use(express.json());


    app.post('/api/login', async (req: Request<any, any, LoginData>, res) => {
        const data = req.body;
        try {
            const user = await userRepo.findOneBy({username: data.username});
            if (!user) return res.status(404).end();
            const comparisonResult = await bcrypt.compare(data.password, user.password);
            if (!comparisonResult) return res.status(401).end();
            const token = jwt.sign({id: user.id}, process.env.JWT_KEY);
            res.status(200).json({
                token
            });
        } catch (error) {
            console.error(error);
            res.status(500).end();
        }
    });

    app.post('/api/user', async (req: express.Request<any, any, LoginData>, res) => {
        const data = req.body;
        try {
            const user = new User();
            user.username = data.username;
            user.password = await bcrypt.hash(data.password, 12);
            await userRepo.save(user);
            const token = jwt.sign({
                id: user.id
            }, process.env.JWT_KEY);
            res.status(201).json({
                token
            });
        } catch (e) {
            console.error(e);
            res.status(500).end();
        }
    });

    app.get('/api/users/:id', async (req, res) => {
        try {
            const user = await userRepo.findOne({where: {
                id: req.params.id
            },
            select: {
                password: false,
                id: true,
                username: true
            }
        });
            if (!user) return res.status(404).end();
            return res.status(200).json(user);
        } catch (error) {
            console.error(error);
            res.status(500).end();
        }
    });

    app.get('/api/me', async (req, res) => {
        const token = req.headers.authorization.split(' ')[1];
        try {
            const id = verify(req.headers.authorization);
            const user = await userRepo.findOne({
                where: {
                    id: id,
                },
                select: {
                    password: false,
                    id: true,
                    username: true
                },
                relations: {
                    statistics: true,
                    tests: {
                        tasks: true
                    }
                }
            });
            if (!user) return res.status(404).end();
            res.status(200).json(user);
        } catch (error) {
            console.error(error);
            res.status(401).end();
        }
    });

    app.get('/api/tests', async (req, res) => {
        try {
            const tests = await testRepo.find({
                relations: {
                    tasks: true
                }
            });
            console.log(JSON.stringify(tests));
            res.status(200).json({
                tests
            });
        } catch (error) {
            console.error(error);
            res.status(500).end();
        }
    });

    app.get('/api/tests/:id', async (req, res) => {
        try {
            const test = await testRepo.findOne({
                where: {
                    id: req.params.id,
                },
                relations: {
                    tasks: true,
                    stats: false,
                    user: true,
                },
                select: {
                    user: {
                        password: false,
                        id: true,
                        username: true
                    },
                    course: true,
                    name: true,
                    id: true,
                    tasks: true,
                }
            });
            if (!test) return res.status(404).end();
            res.status(200).json(test);
        } catch (error) {
            console.error(error);
            res.status(500).end();
        }
    });

    app.post('/api/test/:id/answers', async (req: express.Request<any, any, AnswerData>, res) => {
        const data = req.body;
        console.log(data);
        const token = req.headers.authorization.split(' ')[1];
        try {
            const id = verify(req.headers.authorization);
            const user = await userRepo.findOneBy({
                id
            });
            const test = await testRepo.findOne({
                where: {
                    id: req.params.id,
                },
                relations: {
                    tasks: true,
                }
            });
            let points = 0;
            for (const task of test.tasks) {
                const answ = data.answers.find(el => el.id === task.id);
                if (answ.text === task.answer) points++;
            }
            let result: number = points / test.tasks.length;
            const stats = new TestStats();
            stats.test = test;
            stats.user = user;
            stats.result = result;
            await statsRepo.save(stats);
            res.status(200).json({
                result
            });
        } catch (error) {
            console.error(error);
            res.status(500).end();
        }
    });


    app.get('/api/theory',async (req, res) => {
        try {
            const theory = await theoryRepo.find();
            res.status(200).json({
                theory
            });
        } catch (error) {
            console.error(error);
            res.status(500).end();
        }
    });

    app.post('/api/theory',async (req: express.Request<any, any, CreateTheory>, res) => {
        try {
            const id = verify(req.headers.authorization);
            const user = await userRepo.findOneBy({id: id});
            const theory = new Theory();
            theory.name = req.body.name;
            theory.text = req.body.text;
            theory.course = req.body.course;
            theory.user = user;
            await theoryRepo.save(theory);
            return res.status(201).end();
        } catch (error) {
            console.error(error);
            res.status(500).end();
        }
    });

    app.post('/api/test', async (req: express.Request<any, any, CreateTest>, res) => {
        try {
            console.log(req.body);
            const id = verify(req.headers.authorization);
            const user = await userRepo.findOneBy({id});
            console.log(user);
            const test = new Test();
            test.course = req.body.course;
            test.user = user;
            test.name = req.body.name;
            await testRepo.save(test);
            for (const i of req.body.tasks) {
                const task = new Task();
                task.text = i.text;
                task.test = test;
                task.answer = i.answer;
                await taskRepo.save(task);
            }
            res.status(201).end();
        } catch (error) {
            console.error(error);
            res.status(500).end();
        }
    })

    app.listen(80);


}).catch(error => console.log(error))
